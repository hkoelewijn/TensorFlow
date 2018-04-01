import os
import sys
import glob
import tensorflow as tf
import keras
from keras.datasets import mnist
from keras.layers import Dense, Flatten, Dropout
from keras.layers import Conv2D, MaxPooling2D
from keras.models import Sequential
from keras.models import Model
from keras.layers import Dense, GlobalAveragePooling2D, Dropout
from keras import backend as K
from keras.optimizers import SGD
import matplotlib.pylab as plt
import load_data as ld
from keras.callbacks import TensorBoard

tensorboard = TensorBoard(log_dir='./logs', histogram_freq=0,
                          write_graph=True, write_images=False)

batch_size = 200
epochs = 50
width = 72
height = 40

train_generator = ld.load_data(width, height, 'train', batch_size)
test_generator = ld.load_data(width, height, 'test', batch_size)
input_shape = (width, height, 3)

gpu_options = tf.GPUOptions(per_process_gpu_memory_fraction=0.75)

class AccuracyHistory(keras.callbacks.Callback):
    def on_train_begin(self, logs={}):
        self.acc = []

    def on_epoch_end(self, batch, logs={}):
        self.acc.append(logs.get('acc'))

with tf.Session(config = tf.ConfigProto(gpu_options=gpu_options)) as sess:
    K.set_session(sess)

    model = Sequential()
    model.add(Conv2D(64, kernel_size=(5, 5), strides=(1, 1),
                    activation='relu',
                    input_shape=input_shape))
    model.add(Conv2D(128, kernel_size=(5, 5), strides=(1, 1),
                    activation='relu',
                    input_shape=input_shape))
    model.add(MaxPooling2D())

    model.add(Conv2D(128, kernel_size=(5, 5), strides=(1, 1),
                    activation='relu',
                    input_shape=input_shape))
    model.add(Conv2D(128, kernel_size=(5, 5), strides=(1, 1),
                    activation='relu',
                    input_shape=input_shape))
    model.add(MaxPooling2D())
        
    model.add(Flatten())
    model.add(Dense(800, activation='relu'))
    model.add(Dense(800, activation='relu'))
    model.add(Dropout(0.25))
    model.add(Dense(2, activation='softmax'))

    model.compile(loss=keras.losses.categorical_crossentropy,
                optimizer=keras.optimizers.Adam(),
                metrics=['accuracy'])


    history = AccuracyHistory()

    model.fit_generator(train_generator,
            epochs = epochs,
            validation_data=test_generator,
            class_weight='auto',
            verbose=1,
            callbacks=[history, tensorboard])

    model.save('keras.model')

    score = model.evaluate_generator(test_generator)

print('Test loss:', score[0])
print('Test accuracy:', score[1])

plt.plot(range(1, 51), history.acc)
plt.xlabel('Epochs')
plt.ylabel('Accuracy')
plt.show()