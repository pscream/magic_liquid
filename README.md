# magic_liquid
The repo contains solutions to the task of minimal liquid waste for the given volume and the given sizes of vials.

## The task
Let's imagine some amount of precious liquid is needed. There is a storage of that liquid in the form of vials of different volumes. When a vial has opened it cannot be closed anymore and if the liquid inside isn't used it's lost. Since the liquid is precious the lost remainder must be minimized. Thus it's important to find an optimum solution where either the minimal amount of liquid is lost or, at the best, no liquid is lost at all.
*Input:*
- the required volume of the precious liquid;
- the possible volumes of vials with the precious liquid;
For simplicity, no limits in the number of vials of each volume are considered.

More than one possible algorithm exists to solve the defined task. Each of the possible algorithms is to be implemented in the form of __a strategy__ (*ISolutionStrategy*). The strategy, in turn, is injected into __the builder__ (*ITaskResolverBuilder*), which finally builds an instance of __the resolver__ class (*ITaskResolver*). The resolver class does all the job.  

## Algorithms
- __Brute Force__ (Cartesian product)
The core of this algorithm is to combine combinations of all possible vials of different volumes to fill up the requested volume.
*Steps:*
1. Calculate how many vials of each volume type are required to fulfill the requested volume with one volume type only. It will guarantee the algorithm will fill up the requested volume even if there's only one volume type of vial. Additionally, it limits the number of total iterations and makes the algorithm finite. Those precalculated values are stored into an array so that each element of the array corresponds to each volume type - the volume type array.
2. Create an array of counters for vials of each volume type: the number of elements of this array equals the number of volume types. Initially, the array is set with all zeros.
3. Iterate through all possible vial volume types, starting from zero and ending with the number of maximal possible quantities for the given vial type from the volume type array, created in __Step 1__.
4. After each iteration, increment one element of the array of counters depending on which volume type element from the volume type array is being iterated: the 'index-Zero' element of the array of counters will have a single iteration run (from 0 to the value in the volume type array) while the 'index-One' element will have a run for each quantity from the 'index-Zero' element and so on and so forth. Every time the counter changes its value, all counters with higher indexes must be dropped to 0. The index of the outmost 'iterable' counter must be recalculated and preserved at each iteration through the looping. The 'iterable' means it hasn't completed all its iterations. 
5. After each iteration, store the combination of counters (a snapshot) from the counter array as a possible solution for further analysis.
6. After all possible combinations of all vial volume types have been iterated, stop the loop. In other words, it's when all counters are set with the maximum number of vials of their volume type.
7. Perform an analysis of all stored combinations and find the most efficient one.


Initial state
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |Comments|
|:-:|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|&larr; max vials per volume|
|&nbsp;|-|-|-|-|
|&nbsp;|&nbsp;|-|-|-|
|&nbsp;|&nbsp;|&nbsp;|-|-|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|-|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|0|0|0|0|0|&larr; counters per volume|

Iteration __1__
The outmost 'iterable' counter is *Volume5*
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |
|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|
|&nbsp;|-|-|-|-|
|&nbsp;|&nbsp;|-|-|-|
|&nbsp;|&nbsp;|&nbsp;|-|-|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|-|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|X|
|0|0|0|0|1|

Iteration __2__
The outmost 'iterable' counter is *Volume4*
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |
|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|
|&nbsp;|-|-|-|-|
|&nbsp;|&nbsp;|-|-|-|
|&nbsp;|&nbsp;|&nbsp;|-|-|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|-|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|X|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|X|
|0|0|0|0|2|

Iteration __3__
The outmost 'iterable' counter is *Volume5* (again)
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |
|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|
|&nbsp;|-|-|-|-|
|&nbsp;|&nbsp;|-|-|-|
|&nbsp;|&nbsp;|&nbsp;|-|-|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|-|
|&nbsp;|&nbsp;|&nbsp;|&nbsp;|&nbsp;|
|&nbsp;|&nbsp;|&nbsp;|X|&nbsp;|
|0|0|0|1|0|

Iteration __m__
The outmost 'iterable' counter is *Volume4*
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |
|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|
|&nbsp;|-|-|-|-|
|&nbsp;|&nbsp;|-|-|-|
|&nbsp;|&nbsp;|&nbsp;|-|-|
|&nbsp;|&nbsp;|X|&nbsp;|-|
|&nbsp;|&nbsp;|X|&nbsp;|X|
|&nbsp;|&nbsp;|X|X|X|
|0|0|3|1|2|

Iteration __m+1__
The outmost 'iterable' counter is *Volume5* (again)
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |
|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|
|&nbsp;|-|-|-|-|
|&nbsp;|&nbsp;|-|-|-|
|&nbsp;|&nbsp;|&nbsp;|-|-|
|&nbsp;|&nbsp;|X|&nbsp;|-|
|&nbsp;|&nbsp;|X|X|&nbsp;|
|&nbsp;|&nbsp;|X|X|&nbsp;|
|0|0|3|2|0|

Iteration __n__, n > m
The outmost 'iterable' counter is *Volume2*
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |
|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|
|X|-|-|-|-|
|X|&nbsp;|-|-|-|
|X|&nbsp;|X|-|-|
|X|&nbsp;|X|X|-|
|X|&nbsp;|X|X|X|
|X|X|X|X|X|
|6|1|4|3|2|

Iteration __n+1__, n > m
The outmost 'iterable' counter is *Volume5* (again)
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |
|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|
|X|-|-|-|-|
|X|&nbsp;|-|-|-|
|X|&nbsp;|&nbsp;|-|-|
|X|&nbsp;|&nbsp;|&nbsp;|-|
|X|X|&nbsp;|&nbsp;|&nbsp;|
|X|X|&nbsp;|&nbsp;|&nbsp;|
|6|2|0|0|0|

Final state
| Volume1 | Volume2 | Volume3 | Volume4 | Volume5 |
|:-:|:-:|:-:|:-:|:-:|
|6|5|4|3|2|
|X|-|-|-|-|
| X | X |-|-|-|
| X | X | X |-|-|
| X | X | X | X |-|
| X | X | X | X | X |
| X | X | X | X | X |
|6|5|4|3|2|