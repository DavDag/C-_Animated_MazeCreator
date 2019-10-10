# C-_Animated_MazeCreator
An Animated Maze creator made in C# with SFML.NET

Benchmark on:
* Microsoft Windows 10 Pro
* Intel(R) Core(TM) i7-8750H CPU @ 2.20GHz, 2201 Mhz, 6 core, 12 processori logici
* (RAM)	32,0 GB
* NVIDIA GeForce GTX 1050 Ti

```
// Stat for: 30x30 (900 cells), 650 x 650 (422.500 pixels)
//      I       |      II       |      III      |       V       |
// 16,146637s   |   7,1768s     |   1,130886s   |   0,822788s   |
// 16,179274s   |   7,4383s     |   0,986498s   |   0,814346s   |
// 17,222456s   |   7,1215s     |   1,209963s   |   0,863192s   |

// Stat for: 120x60 (7200 cells), 1820 x 920 (1.674.400‬ pixels)
//      III     |       IV      |       V       |
//  26,266516s  |   24,306644s  |   21,027369s  |
//  28,259577s  |   24,756120s  |   21,581469s  |
//  28,870544s  |   24,538660s  |   21,548016s  |

// Stat for: 200x100 (20000 cells), 1820 x 920 (1.674.400‬ pixels)
//  III - IV    |       V       |  
//  82,68225s   |   63,63615s   |
//  72,45346s   |
//  72,71573s   |
//  65,34935s   |

// Stat for: 300x150 (45000 cells), 920x470 (432.400 pixels)
//      IV      |       V       |
//  33,27181s   |   33,167095s  |
//  32,88598s   |   32,718697s  |
//  32,72191s   |
```
