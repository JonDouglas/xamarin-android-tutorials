# Fragments

## What ##

Fragments are different from activies, in that they are used by activities. Fragments are not a type of container, but rather a `ViewGroup`.

Consider fragments being a reusable UI piece. You can create a fragment with layouts, lifecycle methods, and much more. You can then host that fragment in many of your activities if you want.

We create fragments by extending the base `Fragment` class. You will soon learn the differences between the default native `Fragment` and the `SupportFragment` provided by the Android Support Library.

## Where ##

Typically it's best to put fragments in their own Folder named `Fragments` this is to ensure you do not confuse this code with `Activities`.

## When ##

Fragments should be used **NOW**! They are the new bread & butter of the Android framework.

## Who ##

Fragments will be implemented either by you or a third party.

## Why ##

Various screen sizes are quite prominent in today's Android market. Fragments are an easy way to support various phone and tablet sizes.

## How ##

I have two tutorials written, `StaticFragments` and `DynamicFragments`. Each tutorial will go into details regarding how you can implement Fragments in your application.