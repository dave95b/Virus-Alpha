using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<T,E>  {

    public T Item1 { get; set; }
    public E Item2 { get; set; }

    public Tuple(T item1,E item2)
    {
        Item1 = item1;
        Item2 = item2;
    }

}

public class Tuple<T, E,B>
{

    public T Item1 { get; set; }
    public E Item2 { get; set; }
    public B Item3 { get; set; }

    public Tuple(T item1, E item2, B item3)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
    }

}
