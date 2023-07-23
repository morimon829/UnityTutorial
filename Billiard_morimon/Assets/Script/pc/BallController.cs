using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // メインボール.
    [SerializeField] GameObject mainBall = null;
    // 打つ力.
    [SerializeField] float power = 20f;
    // 方向表示用オブジェクトのトランスフォーム.
    [SerializeField] Transform arrow = null;
    // ボールリスト.
    [SerializeField] List<ColorBall> ballList = new List<ColorBall>();

    //マウスの位置保存
    Vector3 mousePosition = new Vector3();
    //メインボールのリジットボディ
    Rigidbody mainRigid = null;
    //リセット時の為にメインボールの初期位置を保管
    Vector3 mainBallDefaultPosition = new Vector3();

    void Start()
    {
        //mainballのリジットボディを取得
        mainRigid = mainBall.GetComponent<Rigidbody>();
        //現在のmainballの座標を保存(リセット時に使用)
        mainBallDefaultPosition = mainBall.transform.localPosition;
        //方向線を非表示(初期化？)
        arrow.gameObject.SetActive( false );
    }

    // Update is called once per frame
    void Update()
    {
        // メインボールがアクティブなとき.
        if( mainBall.activeSelf == true )
        {
            // マウスクリック開始時.
            if( Input.GetMouseButtonDown(0) == true )
            {
                // 開始位置を保管.
                mousePosition = Input.mousePosition;
                // 方向線を表示.
                arrow.gameObject.SetActive( true );
                Debug.Log( "クリック開始" );
            }

            // マウスクリック中.
            if( Input.GetMouseButton( 0 ) == true )
            {
                // 現在の位置を随時保管.
                Vector3 position = Input.mousePosition;

                // 角度を算出
                // ベクトルの差 = 開始位置-現在の位置
                Vector3 def = mousePosition - position;
                // ベクトルの差から角度を算出
                float rad = Mathf.Atan2( def.x, def.y );
                // 角度を度数に変換
                float angle = rad * Mathf.Rad2Deg;
                // 角度をベクトル型のY軸に保存
                Vector3 rot = new Vector3( 0, angle, 0 );
                // Unity用の「Quaternion」という角度を表す型に保存
                Quaternion qua = Quaternion.Euler( rot );

                // 方向線の位置角度を設定.
                arrow.localRotation = qua;
                arrow.transform.position = mainBall.transform.position;
            }

            // マウスクリック終了時.
            if( Input.GetMouseButtonUp(0) == true )
            {
                // 終了時の位置を保管.
                Vector3 upPosition = Input.mousePosition;

                // 開始位置と終了位置のベクトル計算から打ち出す方向を算出.
                Vector3 def = mousePosition - upPosition;
                Vector3 add = new Vector3( def.x, 0, def.y );

                // メインボールに力を加える.
                mainRigid.AddForce( add * power );

                // 方向線を非表示に.
                arrow.gameObject.SetActive( false );

                Debug.Log( "クリック終了" );
            }
        }    
    } 
        //リセット処理押下時のアクション
    public void OnResetButtonClicked()
    {
        // メインボールをアクティブに
        mainBall.SetActive( true );
        // メインボールの速度を0に.
        mainRigid.velocity = Vector3.zero;
        // メインボールの回転速度を0に.
        mainRigid.angularVelocity = Vector3.zero;
        // メインボールを初期位置に戻す.
        mainBall.transform.localPosition = mainBallDefaultPosition;

        foreach( ColorBall ball in ballList )
        {
            // カラーボールのリセット.
            ball.Reset();
        }
    }
}