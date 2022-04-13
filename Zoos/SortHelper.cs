using Animals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoos
{
    public static class SortHelper
    {
        public static SortResult BubbleSort(this IList list, Func<object, object, int> comparer)
        {
            int swapCounter = 0;
            int compareCounter = 0;

            for (int i = list.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    compareCounter++;
                    if (comparer(list[j], list[j + 1]) > 0)
                    {
                        Swap(list, j, j + 1);
                        swapCounter++;
                    }
                }
            }

            SortResult result = new SortResult
            {
                Objects = list.Cast<object>().ToList(),
                SwapCount = swapCounter,
                CompareCount = compareCounter
            };

            return result;
        }   

        public static SortResult SelectionSort(this IList list, Func<object, object, int> comparer)
        {            
            int swapCounter = 0;
            int compareCounter = 0;

            for (int i = 0; i < list.Count - 1; i++)
            {
                object minimumName;
                minimumName = list[i];

                for (int j = i + 1; j < list.Count; j++)
                {
                    compareCounter++;
                    if (comparer(list[j], minimumName) < 0)
                    {                        
                        minimumName = list[j];
                    }
                }

                if (comparer(list[i], minimumName) != 0)
                {               
                    Swap(list, i, list.IndexOf(minimumName));
                }
            }

            SortResult result = new SortResult
            {
                Objects = list.Cast<object>().ToList(),
                SwapCount = swapCounter,
                CompareCount = compareCounter
            };

            return result;
        }

        public static SortResult InsertionSort(this IList list, Func<object, object, int> comparer)
        {
            int swapCounter = 0;
            int compareCounter = 0;

            object temp;

            for (int i = 1; i <= list.Count - 1; i++)
            {
                temp = list[i];

                compareCounter++;

                for (int j = i - 1; j >= 0 && comparer(list[j], temp) > 0; j--)
                {
                    // swap the current animal with the previous animal and increment the swap count.
                    Swap(list, j, list.IndexOf(temp));
                    swapCounter++;
                }
            }

            SortResult result = new SortResult
            {
                Objects = list.Cast<object>().ToList(),
                SwapCount = swapCounter,
                CompareCount = compareCounter
            };

            return result;
        }

        public static SortResult QuickSort(this IList list, int leftIndex, int rightIndex, SortResult sortResult, Func<object, object, int> comparer)
        {
            // define variables to keep track of the left and right points in the list
            // initialize them to the passed-in indexes
            int leftPointer = leftIndex;
            int rightPointer = rightIndex;

            // find the animal to pivot on (the middle animal in this case)
            object pivotAnimal = list[(leftIndex + rightIndex) / 2];

            // define and initialize a loop variable
            bool done = false;

            // start looping
            while (!done)
            {
                // while the name of the animal at the left pointer spot in the list precedes the pivot animal's name
                while (comparer(list[leftPointer], pivotAnimal) < 0)
                {
                    // increment the left pointer
                    leftPointer++;

                    // increment the sort result's compare count
                    sortResult.CompareCount++;
                }

                // while the pivot animal's name precedes the name of the animal at the right pointer spot in the list
                while (comparer(pivotAnimal, list[rightPointer]) < 0)
                {
                    // decrement the right pointer
                    rightPointer--;

                    // increment the sort result's compare count
                    sortResult.CompareCount++;
                }

                // if the left pointer is less than or equal to the right pointer
                if (leftPointer <= rightPointer)
                {
                    // swap the animals at the left pointer and right pointer spots
                    Swap(list, leftPointer, rightPointer);

                    // increment the sort result's swap count
                    sortResult.SwapCount++;

                    // then increment the left pointer and decrement the right pointer
                    leftPointer++;
                    rightPointer--;
                }

                // if the left pointer is greater than the right pointer,
                // stop the outer while loop
                if (leftPointer > rightPointer)
                {
                    done = true;
                }
            }

            // if the left index is less than the right pointer
            // use recursion to sort the animals within the left index and right pointer
            if (leftIndex < rightPointer)
            {
                QuickSort(list, leftIndex, rightPointer, sortResult, comparer);
            }

            // if the left pointer is less than the right index
            // use recursion to sort the animals within the left pointer and right index
            if (leftPointer < rightIndex)
            {
                QuickSort(list, leftPointer, rightIndex, sortResult, comparer);
            }

            sortResult.Objects = list.Cast<object>().ToList();

            return sortResult;
        }   

        private static void Swap(this IList list, int index1, int index2)
        {
            object temp;

            temp = list[index2];
            list[index2] = list[index1];
            list[index1] = temp;
        }
    }
}
