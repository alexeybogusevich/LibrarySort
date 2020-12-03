using System;
using System.Collections.Generic;

namespace KNU.Algorithms.LibrarySort.Services
{
    public static class LibrarySortService<T>
    {
        public static Comparer<T> Comparer { get; set; }

        private const int gap = 1000;

        public static void Sort(List<T> sequence)
        {
            if (sequence.Count <= 1)
            {
                return;
            }

            var gapped = new T[gap];
            gapped[0] = sequence[0];

            for (int i = 1; i < gapped.Length; i++)
            {
                gapped[i] = default;
            }

            gapped = StartSorting(sequence, gapped);

            for (int i = 0, j = 0; i < sequence.Count; j++)
            {
                if (gapped[j] != null)
                {
                    sequence[i++] = gapped[j];
                }
            }
        }

        private static T[] StartSorting(List<T> sequence, T[] gapped)
        {
            for (int position = 1, goal = 1; position < sequence.Count; goal *= 2)
            {
                for (int i = 0; i < goal; i++)
                {
                    int insertIndex = BinarySearch(gapped, sequence[position]);
                    insertIndex++;

                    if (insertIndex == gapped.Length)
                    {
                        insertIndex--;
                        int freeIndex = insertIndex - 1;

                        while (gapped[freeIndex] != null)
                        {
                            freeIndex--;
                        }

                        for (; freeIndex < insertIndex; freeIndex++)
                        {
                            gapped[freeIndex] = gapped[freeIndex + 1];
                        }
                    }
                    else if (gapped[insertIndex] != null)
                    {

                        int freeIndex = insertIndex + 1;
                        while (freeIndex < gapped.Length && gapped[freeIndex] != null)
                        {
                            freeIndex++;
                        }

                        if (freeIndex == gapped.Length)
                        {
                            insertIndex--;

                            freeIndex = insertIndex - 1;
                            while (gapped[freeIndex] != null)
                            {
                                freeIndex--;
                            }

                            for (; freeIndex < insertIndex; freeIndex++)
                            {
                                gapped[freeIndex] = gapped[freeIndex + 1];
                            }
                        }
                        else
                        {
                            for (; freeIndex > insertIndex; freeIndex--)
                            {
                                gapped[freeIndex] = gapped[freeIndex - 1];
                            }
                        }
                    }
                    gapped[insertIndex] = sequence[position++];

                    if (position >= sequence.Count)
                    {
                        return gapped;
                    }
                }
                gapped = Rebalance(gapped, sequence);
            }
            return gapped;
        }

        private static T[] Rebalance(T[] gapped, List<T> sequence)
        {

            var rebalanced = new T[Math.Min(2 * gapped.Length, gap * sequence.Count)];
            int eps = gap - 1;

            for (int i = gapped.Length - 1, j = rebalanced.Length - 1; i >= 0; i--)
            {
                if (gapped[i] != null)
                {
                    rebalanced[j--] = gapped[i];

                    for (int k = 0; k < eps; k++)
                    {
                        rebalanced[j--] = default;
                    }
                }
            }

            return rebalanced;
        }

        private static int BinarySearch(T[] gapped, T elem)
        {

            int left = 0;
            int mid;
            int right = gapped.Length - 1;

            while (gapped[right] == null)
            {
                right--;
            }

            while (gapped[left] == null)
            {
                left++;
            }

            while (left <= right)
            {
                mid = (left + right) / 2;

                if (gapped[mid] == null)
                {
                    int tmp = mid + 1;

                    while (tmp < right && gapped[tmp] == null)
                    {
                        tmp++;
                    }

                    if (gapped[tmp] == null || Comparer.Compare(gapped[tmp], elem) > 0)
                    {
                        while (mid >= left && gapped[mid] == null)
                        {
                            mid--;
                        }

                        if (Comparer.Compare(gapped[mid], elem) < 0)
                        {
                            return mid;
                        }

                        right = mid - 1;
                    }
                    else
                    {
                        left = tmp + 1;
                    }
                }
                else if (Comparer.Compare(gapped[mid], elem) < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            while (right >= 0 && gapped[right] == null)
            {
                right--;
            }

            return right;
        }
    }
}
