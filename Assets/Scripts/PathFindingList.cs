
using System.Collections.Generic;
using UnityEngine;

// PathFindList
public class PathFindingList {
    public List<NodeRecord> list;

    public PathFindingList() {
        list = new List<NodeRecord>();
    }

    // returns the NodeRecord structure in the list with the lowest costSoFar value and with most advantage points.
    public NodeRecord smallestElement() {
        NodeRecord smallest = list[0];
        foreach(NodeRecord n in list)
        {
            if (n.advantagePoints > smallest.advantagePoints) smallest = n;
            else if (n.advantagePoints == smallest.advantagePoints) {
                if (n.costSoFar < smallest.costSoFar) smallest = n;
            }
        }
        return smallest;
    }

    // returns true only if the list contains a NodeRecord structure 
    // whose node member is equal to the given parameter.
    public bool contains(Node node) {
        foreach(NodeRecord n in list)
        {
            if (n.node == node)
                return true;
        }
        return false;
    }

    // returns the NodeRecord structure from the list whose node member
    // is equal to the given parameter.
    public NodeRecord find(Node node) {
        foreach(NodeRecord n in list)
        {
            if (n.node == node)
                return n;
        }
        return null;
    }

    // add a NodeRecord structure to the list.
    public void add(NodeRecord n) {
        list.Add(n);
    }

    // remove a NodeRecord structure from the list.
    public void remove(NodeRecord n) {
        list.Remove(n);
    }
}