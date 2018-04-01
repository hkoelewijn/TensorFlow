import os
import sys
from keras.preprocessing.image import ImageDataGenerator

def get_nb_files(directory):
  """Get number of files by searching directory recursively"""
  if not os.path.exists(directory):
    return 0
  cnt = 0
  for r, dirs, files in os.walk(directory):
    for dr in dirs:
      cnt += len(glob.glob(os.path.join(r, dr + "/*")))
  return cnt


def load_data(width, height, type, batch_size):
  directory = f'../FORMAT_{width}x{height}'

  # data prep
  generator = ImageDataGenerator()

  return generator.flow_from_directory(
    directory,
    target_size=(width, height),
    batch_size=batch_size,
  )

