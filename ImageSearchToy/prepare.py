import image_helper
import os
import pygame

(max_width, max_height) = image_helper.get_max_size_all_images("./categories")

print("Max_width = {0}".format(max_width))
print("Max_height = {0}".format(max_height))


if (max_width > 1280):
    max_width = 1280
if (max_height > 768):
    max_height = 768 

pygame.init()
screen = pygame.display.set_mode((1280, 768))

for category_dir in image_helper.get_immediate_subdirectories("./categories"):
    dest_path = "./FORMAT_1280x768/" + category_dir

    if (os.path.exists(dest_path)):
        os.remove(dest_path)
    
    os.makedirs(dest_path)

    for file in os.listdir("./categories/" + category_dir):
        input_path = "{0}/{1}/{2}".format("./categories", category_dir, file)
        output_path = "{0}/{1}".format(dest_path, file)
        image_helper.process_image(screen, input_path, output_path, max_width, max_height)

