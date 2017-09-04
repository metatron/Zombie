using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour {
	private CharaData _charData;

	public Transform MaterialListPos;
	public Image ItemImage;
	private GameObject thumbnailObj;

	//ボタンによってContentの中身を変える。ディフォルトはSword
	public GameObject content;

	public BattlePositionController BattlePositionController;

	public Text UnusedExpPoint;

	public Text CharLevel;
	public Text CharExpPoint;
	public Text CharSwdAtkPoint;
	public Text CharGunAtkPoint;
	public Text CharAtkPoint;


	public void InitCharStatus(CharaData charData) {
		gameObject.SetActive (true);
		_charData = charData;
		CharacterLevelSystem.DisplayCharData (_charData);

		//サムネイル準備
		thumbnailObj = UiController.Instance.GetCharThumbnail(charData);
		thumbnailObj.transform.SetParent (ItemImage.transform);
		thumbnailObj.transform.localPosition = new Vector3(0, -28.0f, 0);
		thumbnailObj.transform.localScale = new Vector3(40.0f, 40.0f, 40.0f);

		//素材設置
		ResetStatusPanelItems<SwordData>();

		//表示するパラメータのアップデート
		UpdateCharacterParam ();

		//バトルポジションの初期化
		BattlePositionController.InitButtons(charData);
	}

	public void CloseStatusPanel() {
		//サムネ破棄
		UiController.Instance.ResetContent(thumbnailObj);

		//CharacterPanel側のサムネを復活
		foreach (GameObject thumbnail in UiController.Instance.characterThumbnailList) {
			Utils.ChangeSpriteRendererLayer (thumbnail, 5); //5: UI
		}

		gameObject.SetActive (false);
	}

	public void OnSwordEquip() {
		ResetStatusPanelItems<SwordData>();
	}

	public void OnGunEquip() {
		ResetStatusPanelItems<GunData>();
	}

	public void OnFoodItem() {
		ResetStatusPanelItems<FoodData>();
	}


	private void OnEquiptItem<T>(AbstractData itemData) where T: AbstractData {
		Debug.LogError ("itemData.ID: " + itemData.ID);
		//============= Swordの装備・解除 =============//
		if (typeof(T) == typeof(SwordData)) {
			//持っていた場合解除
			if (!string.IsNullOrEmpty (_charData.SwordID) && itemData.ID == _charData.SwordID) {
				//解除しますかDialog
				UiController.Instance.OpenDialogPanel ("UnEquip " + itemData.Name + "?", 
				//OKを押した場合、装備解除
					() => {
						_charData.SwordID = "";
						//解除して使用可能にする
						PlayerData.UnequipItem (itemData.ID);
						//数値が変わっていた場合コンテンツリセット
						ResetStatusPanelItems<T> ();
					},
				//cancelを押した場合何もなし
					() => {}
				);
			}
			//装備
			else if (PlayerData.GetItemNum (itemData.ID) > 0) {
				UiController.Instance.OpenDialogPanel ("Equip " + itemData.Name + "?", 
				//OKを押した場合、装備
					() => {
						//装備がある場合解除
						if (_charData.SwordID != "") {
							PlayerData.UnequipItem (_charData.SwordID);
						}

						_charData.SwordID = itemData.ID;
						//ストレージから引く
						PlayerData.EquipItem (itemData.ID);
						//数値が変わっていた場合コンテンツリセット
						ResetStatusPanelItems<T> ();
					},
				//cancelを押した場合何もなし
					() => {}
				);
			}
		}

		//============= Gunの装備・解除 =============//
		else if (typeof(T) == typeof(GunData)) {
			//持っていた場合解除
			if (!string.IsNullOrEmpty (_charData.GunID) && itemData.ID == _charData.GunID) {
				//解除しますかDialog
				UiController.Instance.OpenDialogPanel ("UnEquip " + itemData.Name + "?", 
					//OKを押した場合、装備解除
					() => {
						_charData.GunID = "";
						//解除して使用可能にする
						PlayerData.UnequipItem (itemData.ID);
						//数値が変わっていた場合コンテンツリセット
						ResetStatusPanelItems<T> ();
					},
					//cancelを押した場合何もなし
					() => {}
				);
			}
			//装備
			else if (PlayerData.GetItemNum (itemData.ID) > 0) {
				UiController.Instance.OpenDialogPanel ("Equip " + itemData.Name + "?", 
					//OKを押した場合、装備
					() => {
						//装備がある場合解除
						if (_charData.GunID != "") {
							PlayerData.UnequipItem (_charData.GunID);
						}

						_charData.GunID = itemData.ID;
						//ストレージから引く
						PlayerData.EquipItem (itemData.ID);
						//数値が変わっていた場合コンテンツリセット
						ResetStatusPanelItems<T> ();
					},
					//cancelを押した場合何もなし
					() => {}
				);
			}
		}
	}

	private void OnUseItem<T>(AbstractData itemData) where T: AbstractData {
		Debug.LogError ("itemData.ID: " + itemData.ID);
		//============= Foodの消費 =============//
		if (typeof(T) == typeof(FoodData)) {
			//最初数チェック
			int crntNum = PlayerData.GetItemNum (itemData.ID);
			if (crntNum <= 0) {
				UiController.Instance.OpenDialogPanel ("Don't have " + itemData.Name + ".", 
					() => {}
				);

				return ;
			}

			//満腹だった場合は消費しない
			if (_charData.hunger >= 100.0f) {
				UiController.Instance.OpenDialogPanel ("Don't need to eat now.", 
					() => {}
				);

				return ;
			}
				

			//消費して回復
			float recoverVal = CharaData.HUNGER_MAX*((FoodData)itemData).RecoverRatio;
			float tmpHungerLv = recoverVal + _charData.hunger;
			UiController.Instance.OpenDialogPanel ("Do you want to use " + itemData.Name + "?", 
				//YES
				() => {
					PlayerData.UseItem(itemData.ID, 1);
					_charData.hunger = Mathf.Min (CharaData.HUNGER_MAX, tmpHungerLv);
					//数値が変わっていた場合コンテンツリセット
					ResetStatusPanelItems<T> ();
				}, 
				()=> {}
			);
		}//Use Food
	}

	public void OnLevelUpBtn() {
		ExperienceData nextExpData = ExperienceDataTableObject.Instance.Table.All.FirstOrDefault (expData => expData.Level == "" + (_charData.Level+1));
		if (nextExpData == null) {
			UiController.Instance.OpenDialogPanel ("Level is at its Maximum", 
				() => {}
			);
			return;
		}

		//レアリティによって1 ExpPointの価値が変わる
		int necessaryExpPoint = (int)(nextExpData.Exp * _charData.RarityData.CompoCostRatio);
		//次のレベルに必要なExpPointがある
		if (PlayerData.unusedExpPoints >= necessaryExpPoint) {
			UiController.Instance.OpenDialogPanel ("Level Up to " + nextExpData.Level + "\nNeed: " + necessaryExpPoint + " To Level Up.", 
				//レベルアップ
				() => {
					PlayerData.unusedExpPoints -= necessaryExpPoint;
					_charData.Level++;
					_charData.CurrentExp += necessaryExpPoint;

					UpdateCharacterParam ();
				},
				//cancelを押した場合何もなし
				() => {
				}
			);
		}
		//足りなかった場合
		else {
			UiController.Instance.OpenDialogPanel ("Need More Exp Points!", 
				() => {}
			);
		}
	}

	public void UpdateCharacterParam() {
		//プレイヤーのExpPoint表示
		UnusedExpPoint.text = "Unused ExpPoint: " + PlayerData.unusedExpPoints;

		//キャラクタレベル情報
		CharLevel.text = "Lv: " + _charData.Level;
		CharExpPoint.text = "Exp: " + _charData.CurrentExp;
		CharAtkPoint.text = "Atk: " + _charData.CrntAtk();

		CharGunAtkPoint.text = "GUN: Not Equiped";
		if (!string.IsNullOrEmpty (_charData.GunID)) {
//			GunData gunData = GunDataTableObject.Instance.Table.All.FirstOrDefault (tmpData => tmpData.ID == _charData.GunID);
			GunData gunData = GunDataTableObject.Instance.GetParams(_charData.GunID);
			CharGunAtkPoint.text = "GUN: x" + gunData.Damage;
		}

		CharSwdAtkPoint.text = "SWD: Not Equiped";
		if (!string.IsNullOrEmpty (_charData.SwordID)) {
			SwordData swordData = SwordDataTableObject.Instance.Table.All.FirstOrDefault (tmpData => tmpData.ID == _charData.SwordID);
			CharSwdAtkPoint.text = "SWD: x" + swordData.Damage;
		}
	}

	private void ResetStatusPanelItems<T>() where T: AbstractData {
		UiController.Instance.ResetContent(content);
		CraftUiController.Instance.InitItemObjButton<T> (content,
			//ボタンを押した場合は装備or解除
			(AbstractData itemData) => {
				//装備品
				if (typeof(T) == typeof(GunData) || typeof(T) == typeof(SwordData)) {
					OnEquiptItem<T>(itemData);
				}
				//消耗品
				else if(typeof(T) == typeof(FoodData)) {
					OnUseItem<T>(itemData);
				}
					
			}
		);
		UpdateCharacterParam();
	}

}
