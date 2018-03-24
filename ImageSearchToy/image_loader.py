import os
import imageio
import random
import numpy as np
import matplotlib.pyplot as plt

class ImageLoader:
  def __init__(self, directory):
    if not os.path.exists(directory):
      raise Exception(f'Directory {directory} does not exist')

    self.classes = self._get_immediate_subdirectories(directory)
    one_hot_encoded_classes = ImageLoader._one_hot_encode(self.classes)

    if len(self.classes) < 2:
       raise Exception(f'Directory {directory} does not have a minimum of two subdirectories')

    images = []

    for class_name in self.classes:
      file_names = os.listdir(f"{directory}/{class_name}")
      one_hot_encoded_class = one_hot_encoded_classes[class_name]

      print(f'Found {len(file_names)} in directory {class_name}')

      images.extend([(self._read_image(f"{directory}/{class_name}/{file_name}"), one_hot_encoded_class) for file_name in file_names])
      print('')

    random.shuffle(images)
    threshold = len(images) * 7 // 10

    train = images[:threshold]
    test = images[threshold:]

    self.train_x = np.array([t[0] for t in train])
    self.train_y = np.array([t[1] for t in train])
    self.test_x = np.array([t[0] for t in test])
    self.test_y = np.array([t[1] for t in test])

  def GetData(self):
    return self.train_x, self.train_y, self.test_x, self.test_y

  def _get_immediate_subdirectories(self, a_dir):
    return [name for name in os.listdir(a_dir)
            if os.path.isdir(os.path.join(a_dir, name))]
  
  def _read_image(self, file_name):
    result = imageio.imread(file_name)
    print('.', end='', flush=True)
    return result

  @staticmethod
  def _one_hot_encode(values):
    indexed = [i for i, c in enumerate(values)]
    n = len(indexed)
    out = np.zeros((n, len(indexed)))
    out[range(n), indexed] = 1
    result = dict((v, out[values.index(v)]) for v in values)
    return result 

    
if __name__ == "__main__":
  print('Loading...')
  loader = ImageLoader('./FORMAT_72x40')

  print('Train shape')
  print(loader.train_x.shape)
  print(loader.train_y.shape)

  print('Test shape')
  print(loader.test_x.shape)
  print(loader.test_y.shape)
  print(loader.test_y[1])

  # plt.ion()

  # for i in range(len(loader.train_x)):
  #   plt.imshow(loader.train_x[i])
  #   plt.show()
  #   plt.pause(0.01)
  #   plt.clf()