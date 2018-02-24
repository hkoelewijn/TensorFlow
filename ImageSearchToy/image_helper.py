import os
import pygame as pg
import sys

def get_immediate_subdirectories(a_dir):
    return [name for name in os.listdir(a_dir)
            if os.path.isdir(os.path.join(a_dir, name))]

def get_image_size(image_path):
    try:
        image = pg.image.load(image_path)
        return image.get_size()
    except:
        print("Could not process file {0}: {1})".format(image_path, sys.exc_info()[0]))
        os.remove(image_path)
        return (0, 0)
    
def get_max_image_size(category_dir):
    max_width = 0
    max_height = 0
    nr_of_files = 0

    print("Start scanning directory " + category_dir)

    for file in os.listdir(category_dir):
        nr_of_files += 1
        name = "{0}/{1}".format(category_dir, file)
        print("Reading size of file " + name)
        
        (width, height) = get_image_size(name)
        if (width > max_width):
            max_width = width
        if (height > max_height):
            max_height = height

    print("Finished scanning {0} files in  directory {1}".format(nr_of_files, category_dir))
    
    return (max_width, max_height)

def get_max_size_all_images(categories_dir):
    categories = get_immediate_subdirectories(categories_dir)
    max_width_all = 0
    max_height_all = 0
    for category in categories:
        (max_width, max_height) = get_max_image_size(categories_dir + "/" + category)

        if (max_width > max_width_all):
            max_width_all = max_width
        if (max_height > max_height_all):
            max_height_all = max_height

    return (max_width_all, max_height_all)

def process_image(screen, image_path, destination_path, target_width, target_height):
    print("Processing file " + image_path)

    image = pg.image.load(image_path)

    (width, height) = image.get_size()

    image = image.convert(24)

    if (width > target_width or height > target_height):
        print("WARNING: Scaling down image " + image_path)
        scale_x = width / target_width
        scale_y = height / target_height

        scaled_width = 0
        scaled_height = 0

        if (scale_x <= scale_y):
            scaled_width = round(0.5 + width / scale_y)
            scaled_height = target_height
        else:
            scaled_width = target_width
            scaled_height = round(0.5 + height / scale_x)

        image = pg.transform.smoothscale(image, (scaled_width, scaled_height))
    
        print("Scaled down to {0} x {1}".format(scaled_width, scaled_height))
    
    x_offset = (target_width - image.get_width()) / 2
    y_offset = (target_height - image.get_height()) / 2

    pg.image.save(screen, destination_path)

    screen.fill((0, 0, 0))
    screen.blit(image, (x_offset, y_offset))
    pg.display.update()


