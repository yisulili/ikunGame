using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject basketball;
    public bool basketballEnabled;
    public LineRenderer lineb;
    Vector2 ForceDirection;
    Vector3[] lines = new Vector3[10];
    private void OnMouseDown()
    {
        if (basketball != null)
        {
            basketball.transform.eulerAngles = Vector3.zero;
            basketball.GetComponent<Rigidbody2D>().simulated = false;
            basketballEnabled = true;
        }              
    }
    private void OnMouseDrag()
    {
        if (basketballEnabled&& basketball!=null)
        {
            //球和辅助线位置摆放
            basketball.transform.position = new(transform.position.x, transform.position.y + 1.8f, transform.position.z);
            lineb.transform.position = new(transform.position.x, transform.position.y + 1.8f, transform.position.z);
            //辅助线绘制
            basketballLine();
            // 获取鼠标在屏幕上的位置
            Vector3 mousePos = Input.mousePosition;
            // 将鼠标在屏幕上的位置转换为世界空间中的位置        
            ForceDirection = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
            Vector2 ppos = new(transform.position.x, transform.position.y);           
            ForceDirection = (ForceDirection - ppos) * -6;
            ForceDirection =Vector2.ClampMagnitude(ForceDirection,5);
        }
    }
    private void OnMouseUp()
    {
        if (basketballEnabled && basketball != null)
        {
            basketball.GetComponent<Rigidbody2D>().Sleep();
            basketball.GetComponent<Rigidbody2D>().simulated = true;
            basketball.GetComponent<Rigidbody2D>().AddForce(ForceDirection * 1.5f, ForceMode2D.Impulse);
            lineb.positionCount = 0;

            basketballEnabled = false;
            basketball=null;
        }
        
    }
    //球辅助线
    void basketballLine() {
        for (int i = 0; i < 10; i++)
        {
            //曲线绘制公式
            lines[i] = Physics2D.gravity*(0.5f*(i*0.1f)*(i*0.1f))+ ForceDirection*1.5f * i*0.1f;
        }
        lineb.positionCount = 10;
        lineb.SetPositions(lines);
    }

}
