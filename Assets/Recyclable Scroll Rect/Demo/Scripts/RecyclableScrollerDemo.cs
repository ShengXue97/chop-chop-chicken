using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

//Dummy Data model for demostraion
public class ContactInfo
{
    public string Name;
    public string Email;
    public string Score;
    public string id;
}

public class RecyclableScrollerDemo : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    private List<ContactInfo> _contactList = new List<ContactInfo>();


    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        _recyclableScrollRect = GameObject.FindGameObjectWithTag("ScrollView").GetComponent<RecyclableScrollRect>();
        _recyclableScrollRect.DataSource = this;
        GameObject.FindGameObjectWithTag("Leaderboard").SetActive(false);
    }

    //Initialising _contactList with dummy data 
    public void InitData(string data)
    {
        if (_contactList != null) _contactList.Clear();
        if (data != "\"\"")
        {
            data = data.Substring(1, data.Length - 3);

            string[] users = data.Split(';');

            for (int i = 0; i < users.Length; i++)
            {
                string user = users[i];
                string[] details = user.Split(',');
                if (user == "")
                {
                    break;
                }

                ContactInfo obj = new ContactInfo();
                obj.Name = details[0];
                obj.Email = details[1];
                obj.Score = details[3];

                obj.id = "";
                Debug.Log(obj.Name);
                _contactList.Add(obj);
            }

            _contactList.Sort(SortByScore);
        }

        for (int i = 0; i < _contactList.Count; i++)
        {
            _contactList[i].id = "#" + (i + 1).ToString();
        }

        GameObject ScrollView = GameObject.FindGameObjectWithTag("ScrollView");
        if (ScrollView != null)
        {
            ScrollView.GetComponent<RecyclableScrollRect>().Start();
        }

        GameObject LoadingLeaderboard = GameObject.FindGameObjectWithTag("LoadingLeaderboard");
        if (LoadingLeaderboard != null)
        {
            LoadingLeaderboard.SetActive(false);
        }
    }

    static int SortByScore(ContactInfo p1, ContactInfo p2)
    {
        int p1Score = int.Parse(p1.Score);
        int p2Score = int.Parse(p2.Score);
        return p2Score.CompareTo(p1Score);
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _contactList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as DemoCell;
        item.ConfigureCell(_contactList[index], index);
    }

    #endregion
}