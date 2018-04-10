import java.io.BufferedReader;
import java.io.File;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;

public class Main {
    enum sortAlgos { NotYetSet, Insertion, Bubble, Merge };

	public static void main(String[] args) {
        sortAlgos sortAlgo = ParseCmdArgSort(args);
        if (sortAlgo == null) { return; }
        String inputPath = ParseCmdArgPath(args);
        if (inputPath == null) { return; }

        ArrayList<Float> data = ReadInputFile(inputPath);

        switch (sortAlgo)
        {
            case Insertion:
                SortInsertion(data);
                break;
            case Bubble:
                SortBubble(data);
                break;
            case Merge:
                data = SortMerge(data);
                break;
            default:
            	break;
        }

		for (Iterator<Float> i = data.iterator(); i.hasNext();) {
		    Float item = i.next();
		    System.out.println(item);
		}
	}
	
    private static sortAlgos ParseCmdArgSort(String args[])
    {
        sortAlgos sortAlgo = sortAlgos.NotYetSet;

        if (args.length < 2)
        {
            System.out.println("Requires two arguments, only " + args.length + " provided");
            System.out.println("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]");
            return null;
        }

        String argAlgo = args[0];

        switch (argAlgo)
        {
            case "insertion":
                sortAlgo = sortAlgos.Insertion;
                break;
            case "bubble":
                sortAlgo = sortAlgos.Bubble;
                break;
            case "merge":
                sortAlgo = sortAlgos.Merge;
                break;
            default:
                System.out.println(argAlgo + " is not a valid sorting algorithm.");
                System.out.println("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]");
                return null;
        }

        return sortAlgo;
    }
	
    private static String ParseCmdArgPath(String args[])
    {
        String inputPath = "";

        if (args.length < 2)
        {
            System.out.println("Requires two arguments, only " + args.length + " provided");
            System.out.println("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]");
            return null;
        }
        
        String argPath = args[1];
        
        if(!(new File(argPath)).exists() || (new File(argPath)).isDirectory()) {
            System.out.println("Specified input path invalid or does not exist.");
            System.out.println("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]");
            return null;
        }
        inputPath = argPath;

        return inputPath;
    }

    private static ArrayList<Float> ReadInputFile(String inputPath)
    {
        ArrayList<Float> data = new ArrayList<Float>();

        try
        {
        	try (BufferedReader br = Files.newBufferedReader(Paths.get(inputPath), StandardCharsets.UTF_8)) {
        	    for (String line = null; (line = br.readLine()) != null;) {
                    try
                    {
                        data.add(Float.parseFloat(line));
                    }
                    catch (NumberFormatException e)
                    {
                        System.out.println("Error parsing input file at line " + line.toString());
                        System.out.println("Input file must contain one Float per line");
                    }
                }
            }
        }
        catch (Exception e)
        {
            System.out.println("Exception thrown when attempting to read input file");
            System.out.println("Exception message: " + e.getMessage());
            return null;
        }

        return data;
    }

    private static void SortInsertion(ArrayList<Float> data)
    {
        for (int i = 0; i < data.size(); i++)
        {
            for (int j = i; j > 0 && (data.get(j - 1) > data.get(j)); j--)
            {
                Float tmp = data.get(j);
                data.set(j, data.get(j - 1));
                data.set(j - 1, tmp);
            }
        }
    }

    private static void SortBubble(ArrayList<Float> data)
    {
        while (true)
        {
            Boolean noSwaps = true;

            for (int i = 1; i < data.size(); i++)
            {
                if (data.get(i - 1) > data.get(i))
                {
                	Collections.swap(data, i, i-1);
                    noSwaps = false;
                }
            }

            if (noSwaps)
            {
                break;
            }
        }
    }

    private static ArrayList<Float> SortMerge(ArrayList<Float> data)
    {
        if (data.size() <= 1)
        {
            return data;
        }

        ArrayList<Float> left = new ArrayList<Float>();
        ArrayList<Float> right = new ArrayList<Float>();
        for (int i = 0; i < data.size() / 2; i++)
        {
            left.add(data.get(i));
        }
        for (int i = data.size() / 2; i < data.size(); i++)
        {
            right.add(data.get(i));
        }

        left = SortMerge(left);
        right = SortMerge(right);

        return Merge(left, right);
    }

    private static ArrayList<Float> Merge(ArrayList<Float> left, ArrayList<Float> right)
    {
        ArrayList<Float> result = new ArrayList<Float>();

        while (left.size() > 0 && right.size() > 0)
        {
            if (left.get(0) < right.get(0))
            {
                result.add(left.get(0));
                left.remove(0);
            }
            else
            {
                result.add(right.get(0));
                right.remove(0);
            }
        }

        while (left.size() > 0)
        {
            result.add(left.get(0));
            left.remove(0);
        }
        while (right.size() > 0)
        {
            result.add(right.get(0));
            right.remove(0);
        }

        return result;
    }
}
