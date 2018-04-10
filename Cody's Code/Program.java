import java.io.*;
import java.util.*;

public class Program {

	private static ArrayList<Float> readFile(String filename)
	{
		ArrayList<Float> ArrayList = new ArrayList<Float>();
		File file = new File(filename);
		try
		{
			FileReader fileReader = new FileReader(file);
			BufferedReader bufferedReader = new BufferedReader(fileReader);
			String line;
			Float f = 0f;
			while ((line = bufferedReader.readLine()) != null) 
			{
				try
				{
					f = 0f;
					f = Float.parseFloat(line);
				}
				catch(NumberFormatException n)
				{
					System.out.println("Exception in parsing line");
				}
				ArrayList.add(f);
			}
			fileReader.close();
		}
		catch(IOException e)
		{
			System.out.println("Exception in reading file");
		}
		
		return ArrayList;
	}
	
	static void verify(ArrayList<Float> nums)
    {
        Float x = 0f;
        boolean first = true;
        for(int i = 0; i < nums.size(); i++)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                if (nums.get(i) < x)
                {
                   System.out.println("Not sorted");
                }

            }
            System.out.println(nums.get(i).toString());
            x = nums.get(i);
        }
    }
	
	static ArrayList<Float> merge(ArrayList<Float> numbers, int left, int middle, int right)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        ArrayList<Float> L = new ArrayList<Float>();
        ArrayList<Float> R = new ArrayList<Float>();

        int i;
        int j;
        for(i = 0; i < n1; i++)
        {
            L.add(numbers.get(left + i));
        }

        for (j = 0; j < n2; j++)
        {
            R.add(numbers.get(middle + 1 + j));
        }

        i = 0;
        j = 0;
        int k = left;

        while(i < n1 && j < n2)
        {
            if(L.get(i) <= R.get(j))
            {
            	//set(position, value)
            	numbers.set(k, L.get(i));
                i++;
            }
            else
            {
            	numbers.set(k, R.get(j));
                j++;
            }
            k++;
        }

        while(i < n1)
        {
        	numbers.set(k, L.get(i));
            i++;
            k++;
        }

        while (j < n2)
        {
        	numbers.set(k, R.get(j));
            j++;
            k++;
        }

        return numbers;
    }

    //modified from https://www.geeksforgeeks.org/merge-sort/
    static ArrayList<Float> merge_sort(ArrayList<Float> numbers, int left, int right)
    {
        if(left < right)
        {
            int middle = (left + (right - 1)) / 2;
            merge_sort(numbers, left, middle);
            merge_sort(numbers, middle + 1, right);
            merge(numbers, left, middle, right);
        }
        return numbers;
    }
	
	static ArrayList<Float> insertion_sort(ArrayList<Float> numbers)
    {
        int i = 0;
        while (i < numbers.size())
        {
            int j = i;
            while (j > 0 && numbers.get(j - 1) > numbers.get(j))
            {
            	Collections.swap(numbers, j - 1, j);
                j -= 1;
            }
            i += 1;
        }
        return numbers;
    }
	
	static ArrayList<Float> bubble_sort(ArrayList<Float> numbers)
    {
        boolean swapped = true;
        while (swapped) {
            swapped = false;

            int i = 0;

            while (i < numbers.size() - 1)
            {
                if (numbers.get(i) > numbers.get(i + 1))
                {
                	Collections.swap(numbers, i + 1, i);
                    swapped = true;
                }
                i += 1;
            }
        }
        return numbers;
    }
	
	public static void main(String[] args) { 
        ArrayList<Float> numbers = new ArrayList<Float>();
        
        if(args.length > 0)
        {
        	numbers = readFile(args[0]);
        }
        else
        {
        	numbers.add(5f);
        	numbers.add(8f);
        	numbers.add(2f);
        	numbers.add(10f);
        	numbers.add(1f);
        	numbers.add(13f);
        	numbers.add(11f);
        }
        
        int algorithm = 1;
        if(args.length > 1)
        {
        	algorithm = Integer.parseInt(args[1]);
        }
        switch(algorithm)
        {
        	case 1:
        		insertion_sort(numbers);
                verify(numbers);
        		break;
        	case 2:
        		bubble_sort(numbers);
                verify(numbers);
        		break;
        	case 3:
        		merge_sort(numbers, 0, numbers.size() - 1);
                verify(numbers);
        		break;
        	default:
        		System.out.println("Invalid algorithm choice");
        		break;
        }
        
        /*
        insertion_sort(numbers);
        verify(numbers);
        ArrayList<Float> numbers2 = numbers;
        bubble_sort(numbers2);
        verify(numbers2);
        ArrayList<Float> numbers3 = numbers;
        merge_sort(numbers3, 0, numbers3.size() - 1);
        verify(numbers3);*/
	}

}
