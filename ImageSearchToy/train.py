import tensorflow as tf
import matplotlib.pyplot as plt
import tensorflow_helpers as tfh
from image_loader import ImageLoader
import numpy as np
import datetime

width = 72
height = 40

x = tf.placeholder(tf.float32,shape=[None,40,72,3])
y_true = tf.placeholder(tf.float32,shape=[None,2])
hold_prob = tf.placeholder(tf.float32)

loader = ImageLoader("./FORMAT_72x40")

train_x, train_y, test_x, test_y = loader.GetData()

convo_1a = tfh.convolutional_layer(x,shape=[5,5,3,64])
convo_1b = tfh.convolutional_layer(convo_1a,shape=[5,5,64,64])
convo_1_pooling = tfh.max_pool_2by2(convo_1b)

convo_2a = tfh.convolutional_layer(convo_1_pooling,shape=[5,5,64,64])
convo_2b = tfh.convolutional_layer(convo_2a,shape=[5,5,64,64])
convo_2_pooling = tfh.max_pool_2by2(convo_2b)

convo_2_flat = tf.reshape(convo_2_pooling,[-1,18*10*64])

full_layer_one = tf.nn.relu(tfh.normal_full_layer(convo_2_flat,512))

full_layer_two = tf.nn.relu(tfh.normal_full_layer(full_layer_one,256))

full_one_dropout = tf.nn.dropout(full_layer_two,keep_prob=0.25)

y_pred = tfh.normal_full_layer(full_one_dropout,2)

cross_entropy = tf.reduce_mean(tf.nn.softmax_cross_entropy_with_logits(labels=y_true, logits=y_pred))
optimizer = tf.train.AdamOptimizer(learning_rate=0.001)
train = optimizer.minimize(cross_entropy)

init = tf.global_variables_initializer()
config = tf.ConfigProto()
config.gpu_options.per_process_gpu_memory_fraction = 0.6

with tf.Session(config=config) as sess:
    sess.run(tf.global_variables_initializer())
    saver = tf.train.Saver()
    now = str(datetime.datetime.now().time())

    max_accuracy = 0.75

    for i in range(5000):
        sess.run(train, feed_dict={x: train_x, y_true: train_y, hold_prob:0.25})
        
        # PRINT OUT A MESSAGE EVERY 50 STEPS
        if i%10 == 0:
            
            # Test the Train Model
            matches = tf.equal(tf.argmax(y_pred,1),tf.argmax(y_true,1))

            acc = tf.reduce_mean(tf.cast(matches,tf.float32))

            accuracy = sess.run(acc,feed_dict={x:test_x,y_true:test_y,hold_prob:1.0})
            print(f'{now}: Step {i}, accuracy = {accuracy}')

            if (accuracy > max_accuracy):
                saver.save(sess, './model',global_step=i)
                max_accuracy = accuracy

