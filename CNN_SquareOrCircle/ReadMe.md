Example of an extremely simple Convolution Neural Network

The training and test data is generated using PyGame.
It draws a series of squares and circles.
The CNN needs to assess if the image is a square or a circle

The Keras version started from the CNN defined in:
https://github.com/adventuresinML/adventures-in-ml-code/blob/master/keras_cnn.py

The keras network performed really bad (46% accuracy)

After reducing the complexity (reducing number of convolution layers and fully connected layer size) and adding a dropout layer the performance improved dramatically (>90%)

The same happened with the mnist CNN from the tensorflow sites tutorial. Here I aslo needed to increase the learning rate to get an acceptable result.
The performance of the keras version was a lot better than the pure tensorflow one. (> 10x faster)

Dependencies:
numpy
matplotlib
pygame
tensorflow
keras (only for keras_*.py)
