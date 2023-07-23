using UnityEngine;

public class ColorBallPhone : MonoBehaviour
{    
    // リセットのための初期位置.
    Vector3 defaultPosition = new Vector3();
    // リジッドボディ.
    Rigidbody rigid = null;

    // Start is called before the first frame update
    void Start()
    {
        //リジットボディの情報をを取得
        rigid = GetComponent<Rigidbody>();
        //ColorBallの初期位置情報を取得
        defaultPosition = this.transform.localPosition;
        
    }

    //リセットボタン押下処理
    public void Reset()
    {
        // メインボールをアクティブに
        gameObject.SetActive(true);
        //リジッドボディの速度を0にする
        rigid.velocity = Vector3.zero;
        //リジッドボディの回転速度を0にする
        rigid.angularVelocity = Vector3.zero;
        // 初期位置に戻す.
        this.transform.localPosition = defaultPosition;
    }

}
