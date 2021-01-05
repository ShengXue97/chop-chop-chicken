using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class DemoCell : MonoBehaviour, ICell
{
    //UI
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI idLabel;

    //Model
    private ContactInfo _contactInfo;
    private int _cellIndex;
    //This is called from the SetCell method in DataSource
    public void ConfigureCell(ContactInfo contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = contactInfo;

        nameLabel.text = contactInfo.Name;
        scoreLabel.text = contactInfo.Score;
        idLabel.text = contactInfo.id;
    }

}
