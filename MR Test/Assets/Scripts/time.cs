using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time
{
    public int week = 1;
    public int month = 1;
    public int year = 2000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getMonthString(int input)
    {
        string month = "Jan";

        if (input == 0)
        {
            return "Dec";
        }

        if (input == 1)
        {
            return "Jan";
        }
        if (input == 2)
        {
            return "Feb";
        }
        if (input == 3)
        {
            return "Mar";
        }
        if (input == 4)
        {
            return "Apr";
        }
        if (input == 5)
        {
            return "May";
        }
        if (input == 6)
        {
            return "Jun";
        }
        if (input == 7)
        {
            return "Jul";
        }
        if (input == 8)
        {
            return "Aug";
        }
        if (input == 9)
        {
            return "Sep";
        }
        if (input == 10)
        {
            return "Oct";
        }
        if (input == 11)
        {
            return "Nov";
        }
        if (input == 12)
        {
            return "Dec";
        }
        return month; 
    }

    public void nextWeek()
    {
        int m = month;
        int w = week;
        int y = year;

        m = month;
        w = week;
        y = year;

        w++;

        if (w == 5)
        {
            w = 1;
            m++;
        }

        if (m == 13)
        {
            m = 1;
            y++;
        }

        month = m;
        week = w;
        year = y;

        GameObject.Find("dateText").GetComponent<Text>().text = getMonthString(month) + ", Week " + week.ToString() + ", " + year.ToString();
    }

}
