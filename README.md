# Explanation

This is my first attempt at creating a practical C# console application.  I used OpenAI's ChatGPT as my paired programmer, and exponentially increased my C# knowledge.  Essentially, this program inserts an index in a `README.md` file.

## Use Case: Scenario 1

You have a single repo with a bunch of sub-folders representing different projects from multiple instructors.  Each sub-folder has a `README.md` file crediting the instructor.  You wish to provide an index of links to the instructor's website, a live demo, and to the GitHub repo.

## Use Case:  Scenario 2

You have a single repo, from a single instructor, with multiple projects under the root folder.  There is only one `README.md` file in the root.  You wish to provide an index of links to a live demo and your code.

# Requirements and Setup

1. The destination output file must be previously created, and have an `# Index` header.  It's the insertion point for the table.
2. Anything before the `# Index` section will remain; however, *__everything after it will be replaced!__*
3. If you choose to scan for `README.md` files in the subfolders, they must have the format shown below.  `# Provider` and `# Link` are required, using the exact `markdown` syntax shown.

```
# Provider

[Traversy Media](https://www.youtube.com/@TraversyMedia)

# Description

> This crash course will teach you all of the fundamentals of CSS Grid

# Link

[CSS Grid Crash Course](https://www.youtube.com/watch?v=0xMQfnTU6oo)
```