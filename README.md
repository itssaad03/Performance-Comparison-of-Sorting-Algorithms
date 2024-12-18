Parallel Sorting Algorithms Comparison
This project compares the performance of parallel and sequential sorting algorithms—Merge Sort and Heap Sort—on large datasets. The goal is to demonstrate the benefits of parallelizing these algorithms and observe the differences in runtime and memory usage.

Features
Sorting Algorithms: Compares two sorting algorithms:
Merge Sort
Heap Sort
Parallelism: Implements both parallel and sequential versions of each algorithm to compare performance.
Performance Metrics: For each sorting method, the runtime and memory usage are measured and compared for parallel and sequential executions.
How it works
The program generates two random integer arrays of different sizes (one for Merge Sort, one for Heap Sort).
Both sequential and parallel versions of Merge Sort and Heap Sort are executed on their respective arrays.
Performance metrics (runtime and memory usage) are collected using Stopwatch for timing and GC.GetTotalMemory for memory usage.
After both algorithms finish, the program checks if the arrays are sorted identically.
Results are displayed on the console with the following information:
Execution time for both sequential and parallel versions.
Memory usage for both versions.
A comparison to confirm whether both sorting approaches give identical results.
Usage
To run the program, follow these steps:

Clone the Repository

bash
Copy code
git clone https://github.com/yourusername/parallel-sorting-algorithms.git
cd parallel-sorting-algorithms
Build and Run

This project is built using .NET Core/Framework. Ensure you have Visual Studio or Visual Studio Code installed along with the .NET SDK.
Open the project in your preferred IDE and build the solution.
Run the project to compare the performance of sequential and parallel sorting algorithms.
Customize Input You can customize the size of the data arrays by modifying the parameters in the Main() method:

GenerateRandomArray(200000) to generate an array for Merge Sort.
GenerateRandomArray(300000) to generate an array for Heap Sort.
