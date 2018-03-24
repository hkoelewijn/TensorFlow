import image_helper
import os
import pygame
import random

# Commented-out lines are needed if all images are smaller than width/height
# (max_width, max_height) = image_helper.get_max_size_all_images("./categories")

# print("Max_width = {0}".format(max_width))
# print("Max_height = {0}".format(max_height))

width = 299
height = 299
categories_folder = "./categories"
destination_folder_prefix = "FORMAT"
train_test_split_percentage = 70

# if (max_width > width):
#    max_width = width
# if (max_height > height):
#    max_height = height

pygame.init()

screen = pygame.display.set_mode((width, height))

for category_dir in image_helper.get_immediate_subdirectories(categories_folder):
    train_dest_path = f"./{destination_folder_prefix}_{width}x{height}/train/{category_dir}"
    test_dest_path = f"./{destination_folder_prefix}_{width}x{height}/test/{category_dir}"

    if (os.path.exists(train_dest_path)):
        os.remove(train_dest_path)

    if (os.path.exists(test_dest_path)):
        os.remove(test_dest_path)
    
    os.makedirs(train_dest_path)
    os.makedirs(test_dest_path)

    fileNames = os.listdir("{0}/{1}".format(categories_folder, category_dir))
    random.shuffle(fileNames)
    
    train_test_threshold = (train_test_split_percentage * len(fileNames)) // 100

    fileCounter = 0
    for file in fileNames:
        input_path = "{0}/{1}/{2}".format(categories_folder, category_dir, file)

        if fileCounter < train_test_threshold:
            output_path = "{0}/{1}".format(train_dest_path, file)
        else:
            output_path = "{0}/{1}".format(test_dest_path, file)

        image_helper.process_image(screen, input_path, output_path, width, height)

        fileCounter += 1

