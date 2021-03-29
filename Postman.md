**Get Balance** **POST**
```
https://localhost:5001/api/Account/balance
```
__________
```
{
  "userID": 1
}
```


**Get History** **POST**
```
https://localhost:5001/api/Account/history
```
__________
```
{
  "userID": 1,
  "from": "2010-03-29T02:46:25.487Z",
  "to": "2020-03-29T02:46:25.487Z"
}
```

**Get Statistic** **POST**
```
https://localhost:5001/api/Account/statistic
```
__________
```
{
  "onDay": "2011-11-11T02:47:03.626Z"
}
```
**Add Transaction** **PUT**
```
https://localhost:5001/api/Account/add
```
__________
```
{
  "userID": 1,
  "time": "2021-03-29T02:48:46.474Z",
  "amount": 102000,
  "notes": "hello"
}
```