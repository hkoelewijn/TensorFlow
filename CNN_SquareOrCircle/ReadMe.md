Example of an extremely simple Convolution Neural Network

The training and test data is generated using PyGame.
It draws a series of squares and circles.
The CNN needs to assess if the image is a square or a circle

The Keras version started from the CNN defined in:
https://github.com/adventuresinML/adventures-in-ml-code/blob/master/keras_cnn.py

The network performed really bad (46% accuracy

After reducing the complexity (number of convolution layers) and adding a dropout layer
the performance improved dramatically

Dependencies:
pygame
numpy
tensorflow
keras
