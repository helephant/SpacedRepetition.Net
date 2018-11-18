# SpacedRepetition.Net
A .NET library that implements spaced repetition algorithms

Spaced repetition systems (SRS) are a way of effectively learning information by rote. They are useful for situations where you need to remember many different items, for example learning the vocabulary for a new language. 

Spaced repetition aims to make practice more effective by calculating the longest possible interval between revision attempts. This helps because time is not spent reviewing material that is already committed to memory. Studies also suggest that long term memories are formed most effectively if revision is done as close as possible to the point at which the material is forgotten. 

Computers are an ideal tool for doing spaced repetition because they can keep track of all the information needed to work out the interval and then show the material in an optimal order. 

There are a number of different SRS algorithms of varying complexity. SpacedRepetition.Net includes these algorithms:
- SuperMemo2: http://www.supermemo.com/english/ol/sm2.htm

It is also possible to implement your own. 

## Using the library

There is  a [nuget package](https://github.com/helephant/SpacedRepetition.Net) for .NET, so you can install it into your project using the following command:
```
Install-Package SpacedRepetition.Net
```