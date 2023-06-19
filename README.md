# Explanation

This is my first attempt at creating a practical C# console application.  I used OpenAI's ChatGPT as my Personal Programming Jesus (PPJ), which spiritually  increased my C# knowledge.  Essentially, this program loops through a repo, and inserts an index into a `README.md` file at the root of that repo.

## Use Case: Scenario 1 (scan subfolder)

You have a single repo with a bunch of sub-folders representing different projects from multiple instructors.  Each sub-folder has a specially-formatted `README.md` file crediting the instructor.  You wish to provide an index of links to the instructor's YouTube website, a live demo, and your code on GitHub.

## Use Case:  Scenario 2 (don't scan subfolders)

You have a single repo, from a single instructor, with multiple projects under the root folder.  There is only one `README.md` file in the root.  You wish to insert an index table with links to a live demo and your code.

# Requirements and Setup

1. The destination output file must be established beforehand, and have a section at the bottom of the file with an `# Index` header.  This is the insertion point for the table.  Nothing else is required.

```
<!-- Destination File -->

# Header 1

# Header 2

# Index

```

2. Anything before the `# Index` section will remain; however, *__everything after it will be replaced!__*
3. If you choose to scan for `README.md` files in the subfolders, they must have the `markdown` format shown in the example below.  `# Provider` and `# Link` are required.

```
<!-- Example source README.md file -->

# Provider

[Traversy Media](https://www.youtube.com/@TraversyMedia)

# Description

> This crash course will teach you all of the fundamentals of CSS Grid

# Link

[CSS Grid Crash Course](https://www.youtube.com/watch?v=0xMQfnTU6oo)
```