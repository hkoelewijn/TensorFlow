import image_helper
import os
import pygame
import random

width = 72
height = 40
categories_folder = "./categories"
destination_folder_prefix = "FORMAT"

pygame.init()

screen = pygame.display.set_mode((width, height))

for category_dir in image_helper.get_immediate_subdirectories(categories_folder):
    dest_path = f"./{destination_folder_prefix}_{width}x{height}/{category_dir}"

    if (os.path.exists(dest_path)):
        os.remove(dest_path)
    
    os.makedirs(dest_path)

    fileNames = os.listdir("{0}/{1}".format(categories_folder, category_dir))
    
    fileCounter = 0
    for file in fileNames:
        input_path = "{0}/{1}/{2}".format(categories_folder, category_dir, file)

        output_path = "{0}/{1}.jpg".format(dest_path, file)

        image_helper.process_image(screen, input_path, output_path, width, height)

        fileCounter += 1

