import sys

def openFile(fname):
	with open(fname) as file:
		lines = file.readlines()
	# you may also want to remove whitespace characters like `\n` at the end of each line
	lines = [float(x.strip()) for x in lines]
	return lines
	
def verify(numbers):
	x = 0
	first = True
	for n in numbers:
		if first:
			first = False
		else:
			if n < x:
				print("Not sorted")
		print(n)
		x = n

def insertion_sort(numbers):
	i = 0
	while i < len(numbers):
		j = i
		while j > 0 and numbers[j - 1] > numbers[j]:
			# swap (j-1) and j
			x = numbers[j - 1]
			numbers[j - 1] = numbers[j]
			numbers[j] = x
			j -= 1
		i += 1

# Merge the two subarrays
def merge(numbers, left, middle, right):
    n1 = int(middle - left + 1)
    n2 = int(right - middle)

    # create temp arrays
    L = []
    R = []

    # Copy data to L[] and R[]
    for i in range(0 , n1):
        L.append(numbers[left + i])

    for j in range(0 , n2):
        R.append(numbers[middle + 1 + j])

    # Merge the temp arrays back into arr[l..r]
    i = 0     # Initial index of first subarray
    j = 0     # Initial index of second subarray
    k = left  # Initial index of merged subarray

    while i < n1 and j < n2 :
        if L[i] <= R[j]:
            numbers[k] = L[i]
            i += 1
        else:
            numbers[k] = R[j]
            j += 1
        k += 1

    # Copy the remaining elements of L[], if there
    # are any
    while i < n1:
        numbers[k] = L[i]
        i += 1
        k += 1

    # Copy the remaining elements of R[], if there
    # are any
    while j < n2:
        numbers[k] = R[j]
        j += 1
        k += 1    

# modified from https://www.geeksforgeeks.org/merge-sort/
def merge_sort(numbers, left, right):
	if left < right:
		middle = int((left+(right-1))/2)
		# Sort first and second halves
		merge_sort(numbers, left, middle)
		merge_sort(numbers, middle+1, right)
		merge(numbers, left, middle, right)

def bubble_sort(numbers):
	swapped = True
	while swapped:
		swapped = False
		i = 0
		while i < len(numbers) - 1:
			if numbers[i] > numbers[i + 1]:
				x = numbers[i + 1]
				numbers[i + 1] = numbers[i]
				numbers[i] = x
				swapped = True
			i += 1
	
def main():
	nums = None
	if len(sys.argv) > 1:
		# open the file passed in
		nums = openFile(sys.argv[1])
	else:
		nums = [5, 8, 2, 10, 1, 13, 11]
	algorithm = 1
	if len(sys.argv) > 2:
		algorithm = int(sys.argv[2])
	if algorithm == 1:
		insertion_sort(nums)
		verify(nums)
	elif algorithm == 2:
		bubble_sort(nums)
		verify(nums)
	elif algorithm == 3:
		merge_sort(nums, 0, len(nums) - 1)
		verify(nums)
	else:
		print("Invalid algorithm")
	
if __name__ == "__main__":
	main()