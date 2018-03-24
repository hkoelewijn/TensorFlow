import numpy as np

def one_hot_encode(values):
    indexed = [i for i, c in enumerate(values)]
    n = len(indexed)
    out = np.zeros((n, len(indexed)))
    out[range(n), indexed] = 1
    result = dict((v, out[values.index(v)]) for v in values)
    return result 

arr = ('a', 'b')
print(one_hot_encode(arr))
