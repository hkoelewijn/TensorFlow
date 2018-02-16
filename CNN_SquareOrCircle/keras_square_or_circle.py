import numpy as np
import pygame as pg
import tensorflow as tf
import keras
from keras.datasets import mnist
from keras.layers import Dense, Flatten, Dropout
from keras.layers import Conv2D, MaxPooling2D
from keras.models import Sequential
import matplotlib.pylab as plt

batch_size = 128
epochs = 10
width = 64
height = 64
classes = ['square', 'circle']
num_classes = len(classes)

def load_data(num_samples):
    images = []
    labels = []
    for sample in range(0, num_samples):
        choose = np.random.randint(0, 2)
        
        surface = pg.surface.Surface((width, height))

        surface.fill((0, 0, 0))

        size = np.random.randint(10, width-20)
        left = np.random.randint(0, width - size)
        top = np.random.randint(0, width - size)

        if (choose == 0):
            rect = pg.Rect(left, top, size, size)
            pg.draw.rect(surface, (255,255,255), rect)
        else:
            pg.draw.ellipse(surface, (255,255,255), [left, top, size, size])
        
        image_data = pg.surfarray.array3d(surface)
        
        labels.append(choose)
        images.append(image_data)

    return np.array(images), np.array(labels)

print('Generating training data')

(x_train, y_train) = load_data(1000)
(x_test, y_test) = load_data(300)
input_shape = (width, height, 3)

# convert the data to the right type
x_train = x_train.astype('float32')
x_test = x_test.astype('float32')
x_train /= 255
x_test /= 255

print('Completed generation of training data')
print('x_train shape:', x_train.shape)
print(x_train.shape[0], 'train samples')
print(x_test.shape[0], 'test samples')

# convert class vectors to binary class matrices - this is for use in the
# categorical_crossentropy loss below
y_train = keras.utils.to_categorical(y_train, num_classes)
y_test = keras.utils.to_categorical(y_test, num_classes)

model = Sequential()
model.add(Conv2D(8, kernel_size=(5, 5), strides=(2, 2),
                 activation='relu',
                 input_shape=input_shape))
model.add(Conv2D(16, (5, 5), strides=(2, 2), activation='relu'))
model.add(Flatten())
model.add(Dense(200, activation='relu'))
model.add(Dropout(0.25))
model.add(Dense(num_classes, activation='softmax'))

model.compile(loss=keras.losses.categorical_crossentropy,
              optimizer=keras.optimizers.Adam(),
              metrics=['accuracy'])


class AccuracyHistory(keras.callbacks.Callback):
    def on_train_begin(self, logs={}):
        self.acc = []

    def on_epoch_end(self, batch, logs={}):
        self.acc.append(logs.get('acc'))

history = AccuracyHistory()

model.fit(x_train, y_train,
          batch_size=batch_size,
          epochs=epochs,
          verbose=1,
          validation_data=(x_test, y_test),
          callbacks=[history])
score = model.evaluate(x_test, y_test, verbose=0)
print('Test loss:', score[0])
print('Test accuracy:', score[1])
plt.plot(range(1, 11), history.acc)
plt.xlabel('Epochs')
plt.ylabel('Accuracy')
plt.show()