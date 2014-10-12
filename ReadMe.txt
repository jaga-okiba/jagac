TODO
	プロジェクトの開き方
	ASS.zipを展開(パスワードはサイボウズにて)してMoCapとSoundをAssetsに入れる
		(Unity上ですることを推奨)

	キャラクタへの追加方法
		(1)	ReplaySceneを開き，シーン内のReplay -> modelListにキャラを追加
				位置は[0,0,0]，回転[0,0,0]，スケール(おそらく)を合わせる
				モデルの非アクティブにする(インスペクタでチェックを外す)
		(2) ReplaySceneのmodelListをコピーし，PlaySceneへ移動シーンに張り付け
				modelListをReplayの下へ入れる．

				modelListをさらにコピーし，playerListに名前を変える．
				playerListをSKinectFullの下へ．
				playerListの各モデルに plugin/kinect/KinectModelControllerV2を設定
					SWにKiect_Prefab，RootにモデルのRootを設定
		(3) modelListをコピーし，CharaSelectSceneへ移動，modelListを貼り付け．
				modelListをTurnTableの下へ．
				modelListのサイズがなぜか [0.1, 0.1, 0.1]になっているので，
				すべて0.2にしてサイズを合わせる．


unityKinect_0_12_2
	Replayにスタッフちゃんとふきだし追加.

unityKinect_0_12_1
	ReplayにBGが割り当てらえていないのを修正．
	FieldSetの回転をグローバル座標指定に変更．

	// kinect_prefabのON/OFFを制御してエラーでないようにしたかったけど，
	// なんかうまくいかないので，コメントアウト
	//CharaSelectのkinectをControlerの中でON/OFFする．
	//LetsDanceのkinectをMyRecの中でON/OFFする．


unityKinect_0_12_0
	キネクトの保存時に曲の時刻を打刻．
	時刻に合わせて再生．

		動作未チェック

unityKinect_0_11_11
	キャラクタの向き調整(途中)

unityKinect_0_11_10
	看板

unityKinect_0_11_9
	ステージの更新
	プレイ画面のサブカメラの配置
unityKinect_0_11_8
	サウンド再生を実装

unityKinect_0_11_7
	かまくらupdate
	LetsDanceのカメラの設定，キャラクタサイズ

unityKinect_0_11_6
	(全シーン)
	影の濃さを半分に調整
	師匠のマテリアルを自己発光するように修正
	FieldSet.csにDirectional lightに関する動作を追加
	(LetsDanceシーンのみ)
	試験的にDancers > kakumaDance01(師匠)の子にSubCameraを設置

unityKinect_0_11_5
	LetsDanceのキャラクタの小さいのを修正
	また，音楽が再生されないのを修正
	projectが競合してたっぽいのを修正

unityKinect_0_11_4
	こだまさんの背景部分を復帰

unityKinect_0_11_3
	Load画面用のシーンを作ってみる

unityKinect_0_11_2
	キャラ選択のIDのずれを修正
	切り替えの乱数を調整

unityKinect_0_11_1
	キャラ追加が容易になるように調整
		CharaSelectScene
			ControllerでmodelListを自動取得．
		MyReplayでmodelListをgetするようにする(CharaSelectと同じmodelListを子に入れる)

		AssetsのKinectをpluginへ移動(実行の都合)

unityKinect_0_11_0
	時間管理の回りを調整する．
	MyRecのRecorderのターゲットを調整
	MyRecでguiCoutのレンダラ―をStartでtrue，rec開始でfalse
	MyRecのmaxTimeをMyReplayのものを使うようにする．

	MyReplayのmaxTimeをちゃんと使えるようにする．
		maxFrameでの判定はしない．Sensorの方チェックし，最後の姿勢を維持する．

	Recorderオブジェクトの表示をオフ．

	GUIstartの余計なスクリプト削除

unityKinect_0_10_9(2014 10 4 最新)
	新キャラ登場
unityKinect_0_10_8(2014 10 4)
	統合


unityKinect_0_10_7
	猿田版

unityKinect_0_10_6_shibat

unityKinect_0_10_6_shibat
	音楽の再生を修正
		(listenerの近くに置くことにする)
	再生時間を調整
		MyReplayとMyRecでmaxTimeを実装
	再生時のOutOfRangeエラーを飲み込む
		MySensor
	KeyBordとjoysticの双方に対応
		MyReplayのGetButtonDownを修正
		Edit -> ProjectSettings -> Input -> Jump
			(Buffaloコントローラの 4ボタン )

unityKinect_0_10_5
	Playシーンでロードが終わるまで音楽の開始を待つ．

unityKinect_0_10_4
	Replayモードでなんか表示されないのを直す
	(表示順の問題か？)

unityKinect_0_10_3
	Playモードでも背景に過去のキャラを置く

unityKinect_0_10_2
	maxFrameを読み込んだファイルの中の一番小さいものにしてみたけど，OutOfRangeの謎のエラーは改修せず．
	読み込みファイルを最新版の順にする(単に読み込み順を逆にするだけ)
	熟練者も並べてみる
	MyReplayにRECORDフラグを付け，レコード時にも過去のデータが再生されうようにする準備

unityKinect_0_10_1
	シーンの統合は保留．
	リプレイモードで，spaceを押すと選択に行くようにする．
	リプレイモードは長くなるので，最初の20秒でカット

unityKinect_0_10_0
	シーンを統合してリロードが内容にしたSceneを追加 (<= うまくいかず)

unityKinect_0_9_3
	モデル統合

unityKinect_0_9_2
	猿田君レイアウト

unityKinect_0_9_1
	記録してみる

unityKinect_0_9_0
	みんなの分を再生

unityKinect_0_8_8
	modeliListとpositionListを自動化
	11データ読み込んでみる

unityKinect_0_8_7
	Replay時にモデルを読み込む

unityKinect_0_8_6
	replayをどうにかする

unityKinect_0_8_5
	RECのエンドとLetsReplayへの遷移の調整
	loadの調整

unityKinect_0_8_4
	kinectのRECを修正

unityKinect_0_8_3
	kinectの自動指定を復活

unityKinect_0_8_2
	kinectの自動指定を復活(?)

unityKinect_0_8_1
	保存を時刻にする．
	保存先のファイルに「オブジェクト名」の指定の準備
		LetsReplayシーンで，myRecordのファイル一覧を取得し，
		書き込まれているモデル名をprintする．

unityKinect_0_8_0
	大三さんのSoundを復活

unityKinect_0_7_8
	舞台をランダムに設定する．

unityKinect_0_7_7
	サウンドの頭を切る
unityKinect_0_7_6
	サウンドとの同期機能を実装
	MyRecのコメントアウトを削除

unityKinect_0_7_5
	RecTimeをどうにかする
	
unityKinect_0_7_4
	2014 9 20 M班と統合

unityKinect_0_7_3
	熟練者の動きと音楽を再生(途中)

unityKinect_0_7_2
	0_7_1と0_7_1hとの統合、キャラセレクト完成

unityKinect_0_7_1
	rootの設定の自動化

UnityKinect_0_7_0h
	キャラクター選択を縦回転に変更
	human_baseとChoukaisanを追加
	キャラクター数を15と想定して設計


unityKinect_0_7_0
	9/20更新開始
	キャラ選択


unityKinect_0_6_3
	ファイルを指定して再生できるシーンの追加?

UnityKinect_0_6_2
	分が違えば違うファイルになる
	読み込める

UnityKinect_0_6_1
	バグだらけでどうにもならなくなった

UnityKinect_0_6_0
UnityKinect_0_5_4
	gitにアップロード予定

UnityKinect_0_5_3
	再生が重たいバグの修正できたらいいね！
	よかったね！

UnityKinect_0_5_2
	demo用調整
	バックアップ用



UnityKinect_0_5_1
	背景（お堀、建物）の合成

UnityKinect_0_5_0
	リプレイシーンの作成、遷移
	シーンの名前付け

UnityKinect_0_4_3
	シーン移動時の謎の一時停止の未解決(要チェック)

UnityKinect_0_4_2
	自動で録画	and 自動で再生
	SwitcherをMyRecに変更

UnityKinect_0_4_1
	自動で録画

UnityKinect_0_4_0
	aにbを統合
	シーン切り替え後指定時間後もとのシーンに戻る

UnityKinect_0_3_2-a-2
	シーン切り替え後、選んだキャラごとに箱のいろを変える
	cID=
	0:青, 1:緑, 2～4:赤, 

UnityKinect_0_3_2-a
	KinectSave,Loadとの統合
	scene:
		右手で箱を触るとキャラクターが反時計回りする、左手で触るとscene2に切り替わる。
	scene2:
		スペースキーで録画（キューブが赤くなる）、もう一度押すと停止（キューブが青くなる）
		ファイルは../myLog,txtに保存される
		停止後Lキーで再生される。

UnityKinect_0_3_2-b_2
	カウントダウンのエフェクト
	カウントダウンは1秒ごと

UnityKinect_0_3_2-b
	時間で自動遷移
	シーン２に入って3秒後にシーン１へ遷移

UnityKinect_0_3_1
	次のシーンにパラメータを渡す

UnityKinect_0_3_0
	シーンチェンジ

UnityKinect_0_2_3
	配置の調整、スポットライト

UnityKinect_0_2_2
	scriptをプラグインズへ


UnityKinect_0_2_1
	script以外のアセットの整理

UnityKinect_0_2_0
	キネクトのタッチとターンテーブルの合成

UnityKinect_0_1_0

UnityKinect_0_0_４

	以下の設定ファイルを用意
	Hip＿Center SpineRoot
	Spine Spine2
	・・・
	・・・

UnityKinect_0_0_3
	壁の色を変える


UnityKinect_0_0_2
	・着物モデルを消す
	・箱をたたく（各手に透明なBOX）

UnityKinect_0_0_1
	Jaga形式モデルでKinectが使える

UnityKinect_0_0_0
	旧バージョン
	着物のモデルが躍る
-------------------------------------------------
以下github練習スペース

いえ～い(こだま)


