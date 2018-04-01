import sys
import time
import imageio
import tensorflow as tf
import numpy as np

image_path = sys.argv[1]

image = imageio.imread(image_path)
input_data = np.array([image])

print(input_data.shape)

saver = tf.train.import_meta_graph('./model.meta', clear_devices=True)

gpu_options = tf.GPUOptions(per_process_gpu_memory_fraction=0.7)

with tf.Session(config = tf.ConfigProto(gpu_options=gpu_options)) as sess:
    saver.restore(sess, 'model')
    
    predictor = tf.get_collection("prediction")[0]
    x = tf.get_collection("x")[0]

    print(type(x))
    result = sess.run(predictor, feed_dict={x: input_data})

    print(result)

    probability = result[0][1]

    print(probability)
    
model_error = 0.85
model_range = 1 - 2 * (1-model_error)

probability = (1 - model_error) + probability * model_range

print(f"Reporting a probability of {probability}")

with open("result.txt", "w") as text_file:
    text_file.write((str(probability)))
