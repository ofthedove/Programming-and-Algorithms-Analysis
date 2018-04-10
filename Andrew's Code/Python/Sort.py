import sys
from enum import Enum
import os.path
import math

sortAlgos = Enum("sortAlgos", "insertion bubble merge")

def insertionSort(data):
    for i in range(0, len(data)):
        j = i
        while j > 0 and (data[j - 1] > data[j]):
            tmp = data[j]
            data[j] = data[j - 1]
            data[j - 1] = tmp
            j -= 1
    return

def bubbleSort(data):
    while True:
        noSwaps = True

        for i in range(1, len(data)):
            if (data[i - 1] > data[i]):
                tmp = data[i]
                data[i] = data[i - 1]
                data[i - 1] = tmp
                noSwaps = False

        if noSwaps is True:
            break
    return

def mergeSort(data):
    if (len(data) <= 1):
        return data

    left = list()
    right = list()

    for i in range(0, math.floor(len(data)/2)):
        left.append(data[i])

    for i in range(math.floor(len(data)/2), len(data)):
        right.append(data[i])

    left = mergeSort(left)
    right = mergeSort(right)

    return merge(left, right)

def merge(left, right):
    result = list()

    while (len(left) > 0 and len(right) > 0):
        if (left[0] < right[0]):
            result.append(left[0])
            del left[0]
        else:
            result.append(right[0])
            del right[0]

    while (len(left) > 0):
        result.append(left[0])
        del left[0]

    while (len(right) > 0):
        result.append(right[0])
        del right[0]

    return result

# Main entry point
if (len(sys.argv) < 2):
    print("Requires two arguments, only " + str(len(sys.argv)) + " provided")
    print("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]")
    sys.exit()

argAlgo = sys.argv[1]
argPath = sys.argv[2]

sortAlgo = sortAlgos.insertion

if (argAlgo == "insertion"):
    sortAlgo = sortAlgos.insertion
elif (argAlgo == "bubble"):
    sortAlgo = sortAlgos.bubble
elif (argAlgo == "merge"):
    sortAlgo = sortAlgos.merge
else:
    print(argAlgo + " is not a valid sorting algorithm.")
    print("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]")
    sys.exit()

if (os.path.isfile(argPath) == False):
    print("Specified input path invalid or does not exist.")
    print("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]")
    sys.exit()

inputPath = argPath


with open(inputPath) as f:
    data = f.readlines()
data = [float(x.strip()) for x in data]

if (sortAlgo == sortAlgos.insertion):
    insertionSort(data)
elif (sortAlgo == sortAlgos.bubble):
    bubbleSort(data)
elif (sortAlgo == sortAlgos.merge):
    data = mergeSort(data)

#for x in range(0, len(data)):
#    print(data[x])