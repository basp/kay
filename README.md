# Kay
**Kay** is a dynamic and concatenative language that is heavily inspired by **Joy** and **XY**. Parts of the implementation are based on **Thun** which is also a Joy implementation.

It is named after [Kathleen Rita Antonelli](https://en.wikipedia.org/wiki/Kathleen_Antonelli). She was one of the six original programmers of the [ENIAC](https://en.wikipedia.org/wiki/ENIAC) and known as **Kay McNulty**, an Irish-born American computer programmer. It is also a firm nod to [Alan Kay](https://en.wikipedia.org/wiki/Alan_Kay) who - even though I never had to fortune to meet him - changed my professional life.

## disclaimer
Kay is just a toy (for now). Unless you are absolutely crazy you really should not use this for any kind of serious stuff. It meant like a vehicle to learn about and experiment with some of the ideas that **Manfred von Thun** was having fun with when he designed **Joy**.

## introduction
Kay is capable of some pretty weird things. Let's show off the most useless program ever. This doesn't calculate anything useful but it shows off some of her symbolic computation capabilities and the crazy ways you can manipulate the interpreter.

We'll try to define the symbol `2 * (3 + 4)` just because we are mad. Note that this name has spaces in it so its extremely unlikely the following will work:
```
kay> 2 * (3 + 4) == 4 3 + 2 *.

line 1:12 mismatched input '==' expecting '.'
Runtime exception: two arguments needed for `*`
```

Indeed she does not like this formulation.

> I'm actually a bit surprised this gives a runtime **and** a parser error. This needs some looking into. 

No sane programming language would accept this though so it is a good thing Kay does not either.

However, we are not defeated yet. Since we have `intern` which converts a string to a symbol, **quotations** which are basically lists that act like a program and `def` which takes two items from the stack and converts them into a *user-defined* definition we can continue. Lets force our way:
```
kay> "2 * (3 + 4)" intern. // 1

2 * (3 + 4) <- top
```

At this point we managed to push our weird symbol name onto the stack as a symbol. We know it's a symbol since it really cannot be anything else by the way it is printed. However, we can check if we want to know for sure. The `name` operation converts the value on top of the stack into a string representation. For symbols, this would return the name of the symbol as a string.

So with the symbol `2 * (3 + 4)` on top of the stack we expect this to be turned into the string `"2 * (3 + 4)"`. Let's see what happens:
```
kay> name.

"2 * (3 + 4)" <- top
```

Indeed we get back the string that represents our symbol name. We can easily turn it back into a symbol again by evaluating another `intern` operation.
```
kay> intern.

2 * (3 + 4) <- top
```

So now that we are sure we can use this crazy symbol name we can push the body:
```
kay> [4 3 + 2 *].          // 2

[4 3 + 2 *] <- top
2 * (3 + 4)
```

We now have two things on the stack, our (crazy) symbol name `2 * (3 + 4)` and a quotation that represents the body for that symbol. Is the body even correct? Does it correctly evaluate the expression `2 * (3 + 4)`? This should evaluate to `14` but we haven't really bothered to check. Let's do that right now using the `x` combinator. This will evaluate the program on top op the stack without removing it.
```
kay> x.

14          <- top
[4 3 + 2 *]
2 * (3 + 4)
```

It correctly evaluates to `14` so it seems the body for our defintion is fine. However we now have this `14` sitting on top of the stack which we need to remove otherwise we cannot invoke the `def` operation which expects a quotation on top and a symbol below. But since we are here we just go ahead and issue a `def` anyway to see what happens:
```
kay> def.

Runtime exception: quotation as first argument needed for `def`
```

Yep that was more or less what was to be expected. Luckily the stack did not get too mutilated but we still have that useless `14` sitting on top.
```
kay> .

14          <- top
[4 3 + 2 *]
2 * (3 + 4)
```

We can get rid of it by evaluating a `pop` operation:
```
kay> pop.

[4 3 + 2 *] <- top
2 * (3 + 4)
 ```

And now we are ready to issue the infamous `def` which will bind the symbol `2 * (3 + 4)` with the body `[4 3 + 2 *]` in the interpreter environment:
```
kay> def.                  // 3
```
Sadly we do not get any great song or fanfare from `def` when it succeeds. It just silently does its job and only gives feedback when things go wrong.

1. We use `intern` to turn the string on top of the stack into a symbol.
2. We push the *body* (the definition) for this symbol onto the stack as a *quotation*.
3. We evaluate `def` in order to take the symbol and definition and add a new user-defined entry to the interpreter environment.

Did this actually work? Well, we can check:
```
kay> "2 * (3 + 4)" intern body.

[4 3 + 2 *] <- top
```

Evaluating `body` will push the definition of the symbol `2 * (3 + 4)` onto the stack. And we do get back the *quotation* we pushed earlier so this seems to work. Now how to call it?
```
kay> "2 * (3 + 4)" intern. // 1

2 * (3 + 4) <- top

kay> body.                 // 2

[4 3 + 2 *] <- top

kay> i.                    // 3

14          <- top
```
So what is happening here?
1. We just use `intern` to make our symbol and push it onto the stack.
2. We use `body` to push the definition of that symbol onto the stack.
3. We use `i` to evaluate the *quotation* that is on top of the stack.

If we execute this program using `trace` we can get some more insight:
```
kay> ["2 * (3 + 4)" intern body i] trace.

===========================================
        stack : queue
-------------------------------------------
              : "2 * (3 + 4)" intern body i
"2 * (3 + 4)" : intern body i
  2 * (3 + 4) : body i
  [4 3 + 2 *] : i
              : 4 3 + 2 *
            4 : 3 + 2 *
          4 3 : + 2 *
            7 : 2 *
          7 2 : *
           14 :
-------------------------------------------

14          <- top
```

The `trace` combinator takes a quotation on top of the stack and executes it like a program while recording the stack and queue states for every step. At the end it will print a formatted trace of each step before the contents of the stack are printed.

Invoking `body` seems a bit of a cheat. Can we invoke the symbol directly? We could do the following:
```
kay> kay> ["2 * (3 + 4)" intern unit i] trace.

===========================================
        stack : queue
-------------------------------------------
              : "2 * (3 + 4)" intern unit i
"2 * (3 + 4)" : intern unit i
  2 * (3 + 4) : unit i
[2 * (3 + 4)] : i
              : 2 * (3 + 4)
              : 4 3 + 2 *
            4 : 3 + 2 *
          4 3 : + 2 *
            7 : 2 *
          7 2 : *
           14 :
-------------------------------------------

14          <- top
```

Here we use `unit` to convert the symbol into a *quotation* which we then evaluate using the `i` combinator. But how much of an improvement is this? 

If we evaluate the trace we can see that even though `unit` is theoretically a bit cleaner, in practice using `body` requires less instructions to be queued. This is caused by `body` directly putting the term associated with symbol `2 * (3 + 4)` on the stack while the `unit` construction causes the term associated with the symbol `2 * (3 + 4)` to be prepended onto the queue.

## dynamic defs
Dynamic definitions is a feature that Joy does not have. In Joy you can have static definitions which you can duplicate and change if you want but you cannot undefine and redefine definitions dynamically - they are baked in at read time. In Kay, built-in definitions cannot be changed but all user-definitions can be defined and undefined at runtime using `def` and `undef` with operands on the stack like any other operation.

> Note that you currently cannot redefine a symbol. You will first have to explicitly undefine it before you can define it again. There might be a `redef` operation in the future.

A traditional Joy definition looks like `symbol == ...` and these are handled differently by the interpreter in the sense that they are not evaluated. The interpreter will treat the dots `...` like a list of factors (i.e. a term) and associate `symbol` with this term in the interpreter environment. These definitions are baked in at read time on what amounts to parser level.

In other words, if you do something like this:
```
kay> A == 2 3 +.
```

What really happens is that the quotation `[2 3 +]` is associated with symbol `A` in the interpreter environment. This bypasses all normal evaluation rules.

In Kay, there is another (dynamic) way to create a definition using the normal evaluation rules and a new combinator called `def` (which combines with `undef` to undefine symbols).

The static `A == 2 3 +` definition from above is equivalent to the following dynamic definition:
```
kay> "A" intern [2 3 +] def.

kay> A.

5           <- top
```

Note that even though this definition is dynamic, The `def` operation will **not** evaluate the quotation `[2 3 +]`. It will just accept this quoted and associate it with the symbol that was just below it on the stack. The definition will only be evaluated when it it actually used in the queue.

We can evaluate the behavior using `trace` as is shown in the following example:
```
kay> [A] trace.

===========
stack : queue
-----------
    : A
    : 2 3 +
  2 : 3 +
2 3 : +
  5 :
-----------

5           <- top
```

The symbol `A` is unrolled at the last moment when it was encountered by the interpreter and it had no other choice but to look it up in its environment.

At the moment it's not quite clear how useful this is but it at least allows you to do funky things:
```
kay> ["A" intern [3 2 +] def A A +] trace.

========================================
    stack : queue
----------------------------------------
          : "A" intern [3 2 +] def A A +
      "A" : intern [3 2 +] def A A +
        A : [3 2 +] def A A +
A [3 2 +] : def A A +
          : A A +
          : 3 2 + A +
        3 : 2 + A +
      3 2 : + A +
        5 : A +
        5 : 3 2 + +
      5 3 : 2 + +
    5 3 2 : + +
      5 5 : +
       10 :
----------------------------------------
```

This creates a program that defines `A` and uses it to perform some computations.

If at some point we want to *undefine* the symbol `A` we can use the `undef` operation. This will take a string or a symbol on top of the stack. Note that you cannot redefine (overwrite) an existing definition. You will have to use `undef` first.
```
kay> A == 3 2 +.

kay> "A" intern user.

true        <- top

kay> "A" intern [4 5 *] def.

Runtime exception: `A' is already defined as: 3 2 +
kay> pop.

kay> "A" undef.

kay> "A" intern user.

false       <- top
```

## examples
Below are just some samples of what kind of symbolics you can do with Kay.

```
kay> S == dup *
      .

kay> A == S.

kay> X == A.

kay> [2 X] trace.

===========
stack : queue
-----------
    : 2 X
  2 : X
  2 : A
  2 : S
  2 : dup *
  2 : 2 *
2 2 : *
  4 :
-----------

4           <- top
```

## references
* [Joy](https://hypercubed.github.io/joy/joy.html)
* [The Theory of Concatenative Combinators](http://tunes.org/~iepos/joy.html)
* [Kitten](https://kittenlang.org/)
* [Joy on Hacker News](https://news.ycombinator.com/item?id=17685548)
* [Thun](http://joypy.osdn.io/index.html)
* [The Concatenative Language XY](https://www.nsl.com/k/xy/xy.htm)
* [Mal](https://github.com/kanaka/mal)