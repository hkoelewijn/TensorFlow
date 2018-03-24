import os
import sys
import glob
import keras
from keras.datasets import mnist
from keras.layers import Dense, Flatten, Dropout
from keras.layers import Conv2D, MaxPooling2D
from keras.models import Sequential
from keras.models import Model
from keras.layers import Dense, GlobalAveragePooling2D, Dropout
from keras.optimizers import SGD
import matplotlib.pylab as plt
import load_data as ld
from keras.callbacks import TensorBoard

tensorboard = TensorBoard(log_dir='./logs', histogram_freq=0,
                          write_graph=True, write_images=False)

batch_size = 200
epochs = 50
width = 640
height = 640

train_generator = ld.load_data(width, height, 'train', batch_size)
test_generator = ld.load_data(width, height, 'test', batch_size)
input_shape = (width, height, 3)

model = Sequential()
model.add(Conv2D(16, kernel_size=(5, 5), strides=(2, 2),
                 activation='relu',
                 input_shape=input_shape))
model.add(Conv2D(16, (5, 5), strides=(2, 2), activation='relu'))
model.add(Conv2D(16, (5, 5), strides=(2, 2), activation='relu'))
model.add(Conv2D(16, (5, 5), strides=(2, 2), activation='relu'))
model.add(Flatten())
model.add(Dense(800, activation='relu'))
model.add(Dropout(0.25))
model.add(Dense(2, activation='softmax'))

model.compile(loss=keras.losses.categorical_crossentropy,
              optimizer=keras.optimizers.Adam(),
              metrics=['accuracy'])


class AccuracyHistory(keras.callbacks.Callback):
    def on_train_begin(self, logs={}):
        self.acc = []

    def on_epoch_end(self, batch, logs={}):
        self.acc.append(logs.get('acc'))

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