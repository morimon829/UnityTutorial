using System.Collections.Generic;
using UnityEngine;

public class BallControllerPhone : MonoBehaviour
{
    // メインボール.
    [SerializeField] GameObject mainBall = null;
    // 打つ力.
    [SerializeField] float power = 20f;
    // 方向表示用オブジェクトのトランスフォーム.
    [SerializeField] Transform arrow = null;
    // ボールリスト.
    [SerializeField] List<ColorBall> ballList = new List<ColorBall>();

    //マウスの位置保存(タッチ用)
    Vector3 startTouch = new Vector3();
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

    void Update()
    {
        // メインボールがアクティブなとき.
        if( mainBall.activeSelf == true )
        {
            // タッチしている指の数が０より多い.
            if( Input.touchCount > 0 )
            {
                // タッチしている指の最初の値を取得.
                Touch touch = Input.GetTouch(0);
                // タッチ処理.
                if( touch.phase == TouchPhase.Began )
                {
                    Debug.Log( "タッチ開始" );
                    // 開始位置を保管.
                    startTouch = touch.position;
                    // 方向線を表示.
                    arrow.gameObject.SetActive( true );
                }
                else if( touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary )
                {
                    Debug.Log( "タッチ中" );
                    // 現在の位置を随時保管.
                    Vector3 position = touch.position;

                    // 角度を算出.
                    Vector3 def = startTouch - position;
                    float rad = Mathf.Atan2( def.x, def.y );
                    float angle = rad * Mathf.Rad2Deg;
                    Vector3 rot = new Vector3( 0, angle, 0 );
                    Quaternion qua = Quaternion.Euler( rot );
                    // 方向線の位置角度を設定.
                    arrow.localRotation = qua;
                    arrow.transform.position = mainBall.transform.position;
                }
                else if( touch.phase == TouchPhase.Ended )
                {
                    Debug.Log( "タッチ終了" );
                    // 終了時の位置を保管.
                    Vector3 upPosition = touch.position;

                    // 開始位置と終了位置のベクトル計算から打ち出す方向を算出.
                    Vector3 def = startTouch - upPosition;
                    Vector3 add = new Vector3( def.x, 0, def.y );

                    // メインボールに力を加える. 
                    mainRigid.AddForce( add * power );

                    // 方向線を非表示に.
                    arrow.gameObject.SetActive( false );
                    Debug.Log( "クリック終了" );
                }
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