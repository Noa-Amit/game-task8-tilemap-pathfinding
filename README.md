<div dir='rtl' lang='he'>

# מטלה 8 סעיפים ד+ו

הוספתי לפרויקט מהשיעור תיקייה ובה שמתי את הסקריפטים החדשים שכתבתי

https://github.com/Noa-Amit/game-task8-tilemap-pathfinding/tree/main/Assets/Scripts/randomMap

עשיתי בפרויקט מספר שינויים:
1. הוספתי לשחקן rigidbody, collider
2. הוספתי אוביקט חדש בשם trigger  שכשהשחקן מגיע אליו המפה משתנה והוא חוזר לנקודת ההתחלה
לאוביקט הוספתי סקריפט בשם NewMapTrigger
3. הוספתי ל tilemap את הסקריפט החדש שכתבתי NewTilemapCaveGenerator

הסקריפטים שכתבתי:

1. NewMapTrigger- כשהשחקן נוגע בטריגר המשחק מתחיל מחדש, כלומר המפה משתנה והשחקן חוזר לנקודת ההתחלה
https://github.com/Noa-Amit/game-task8-tilemap-pathfinding/blob/main/Assets/Scripts/randomMap/newMapTrigger.cs

2. NewTilemapCaveGeneration- יוצר אוביקט מערה שמתאחלת מפה, ואת המפה מציג למסך. במפה יש 3 סוגי אריחים
https://github.com/Noa-Amit/game-task8-tilemap-pathfinding/blob/main/Assets/Scripts/randomMap/NewTilemapCaveGenerator.cs

3. NewCaveGenerator-אוביקט שיוצר מפה רנדומלית חדשה. הקוד כתוב באופן כללי כך שמי שקורא לאוביקט מחליט מה יהיה מספר האריחים על המפה
האוביקט מקבל מערך של מספרים שמייצגים את ההסתברות להופעת כל אריח על המפה ולפי הנתונים האלו מאתחל מפה
https://github.com/Noa-Amit/game-task8-tilemap-pathfinding/blob/main/Assets/Scripts/randomMap/NewCaveGenerator.cs
