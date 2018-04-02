/// <summary>
/// PriorityQueue class
/// </summary>

namespace I2P_Project.Classes
{
    class PriorityQueue<Generic>
    {
        private int length = 0;  // length of a queue
        private Node first = null;  // first element in queue
        private Node rare = null;  // last element in queue

        class Node  // element coneluctor
        {
            public Generic element;  // user_id
            public int priority_level;
            public Node link;  // link cell to next element of queue
        }

        /// <summary>
        /// Enqueue method
        /// </summary>

        public void Push(Generic el, int priority)
        {
            Node temp = first;
            Node prev_node = first;  // previous node
            Node toAdd = new Node
            {
                element = el,
                priority_level = priority
            };

            if (temp == null)
            {
                first = toAdd;
                length++;
                return;
            }

            while (true)  // move elements in the queue according to priority
            {
                if (priority > temp.priority_level)
                {
                    if (temp.link != null)
                    {
                        prev_node = temp;
                        temp = temp.link;
                    }
                    else
                    {
                        temp.link = toAdd;
                        rare = temp.link;
                        break;
                    }
                }
                else if (priority < temp.priority_level)
                {
                    if (temp == first)
                    {
                        toAdd.link = first;
                        first = toAdd;
                    }
                    else
                    {
                        prev_node.link = toAdd;
                        toAdd.link = temp;
                    }
                    break;
                }
                else if (priority == temp.priority_level)
                {
                    if (temp.link != null)
                    {
                        while (priority == temp.priority_level)
                        {
                            if (temp.link != null)
                            {
                                prev_node = temp;
                                temp = temp.link;
                            }
                            else
                            {
                                temp.link = toAdd;
                                length++;
                                return;
                            }
                        }
                        toAdd.link = prev_node.link;
                        prev_node.link = toAdd;
                        break;
                    }
                    else
                    {
                        temp.link = toAdd;
                        rare = temp.link;
                        break;
                    }
                }
            }
            length++;  // increase queue length by 1
        }

        /// <summary>
        /// Dequeue method
        /// </summary>

        public Generic Pop()
        {
            if (length > 0)
            {
                Generic el = first.element;
                first = first.link;
                length--;
                return el;  // returns element data
            }
            else
            {
                return default(Generic);
            }
        }

        /// <summary>
        /// Legth of the queue
        /// </summary>

        public int Length()
        {
            return length;
        }

        /// <summary>
        /// Checking empty in queue
        /// </summary>

        private bool IsEmpty()
        {
            return length == 0;
        }

        /// <summary>
        /// Returns first element in queue
        /// </summary>

        public Generic GetFirstElement()
        {
            return first.element;
        }
    }
}
