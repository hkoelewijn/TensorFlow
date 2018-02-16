import numpy as np
import pygame as pg

def load_data(num_samples, width, height):
    images = []
    labels = []
    for sample in range(0, num_samples):
        choose = np.random.randint(0, 2)
        
        surface = pg.surface.Surface((width, height))

        surface.fill((0, 0, 0))

        size = np.random.randint(5, width-10)
        left = np.random.randint(0, width - size)
        top = np.random.randint(0, width - size)

        if (choose == 0):
            rect = pg.Rect(left, top, size, size)
            pg.draw.rect(surface, (255,255,255), rect)
        else:
            pg.draw.ellipse(surface, (255,255,255), [left, top, size, size])
        
        image_data = pg.surfarray.array3d(surface)
        image_data = image_data.astype('float32')
        image_data /= 255

        labels.append(choose)
        images.append(image_data)

    return np.array(images), np.array(labels)