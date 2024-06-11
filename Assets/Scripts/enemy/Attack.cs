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
            //��͸�����λ�ðڷ�
            basketball.transform.position = new(transform.position.x, transform.position.y + 1.8f, transform.position.z);
            lineb.transform.position = new(transform.position.x, transform.position.y + 1.8f, transform.position.z);
            //�����߻���
            basketballLine();
            // ��ȡ�������Ļ�ϵ�λ��
            Vector3 mousePos = Input.mousePosition;
            // ���������Ļ�ϵ�λ��ת��Ϊ����ռ��е�λ��        
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
    //������
    void basketballLine() {
        for (int i = 0; i < 10; i++)
        {
            //���߻��ƹ�ʽ
            lines[i] = Physics2D.gravity*(0.5f*(i*0.1f)*(i*0.1f))+ ForceDirection*1.5f * i*0.1f;
        }
        lineb.positionCount = 10;
        lineb.SetPositions(lines);
    }

}
