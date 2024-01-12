# Stable Diffusion Web UI - txt2img API Sample
Stable Diffuion Wen UI の txt2img API を使用して 画像を生成・表示する WPF アプリケーション
## Stable Diffusion Web UI の設定  
https://github.com/AUTOMATIC1111/stable-diffusion-webui?tab=readme-ov-file#automatic-installation-on-windows  
に沿ってインストール・設定を行う
1. Python 3.10.6 をインストール  
https://www.python.org/downloads/release/python-3106/ より Python 3.10.6 をインストールする  
インストールする際に "Add Python to PATH" にチェックを入れる
2. git をインストール
3. Stable Diffuion Web UI をクローン  
任意のフォルダでクローンする  
この中に実行するためのファイル一式が格納されている
```
git clone https://github.com/AUTOMATIC1111/stable-diffusion-webui.git
```
4. bat ファイルの編集
"webui-user.bat" を以下のように変更する
```
@echo off

set PYTHON=
set GIT=
set VENV_DIR=
set COMMANDLINE_ARGS=--api --listen --autolaunch
rem set COMMANDLINE_ARGS=

call webui.bat
```

### ポートの開放
そのまま実行すると内部からしか API を叩くことができないのでポートの開放を行う  
"セキュリティが強化された Windows Defender ファイアウォール" ＞ 受信の規則 ＞ 新しい規則 を選択
- 通信の種類： "ポート"
- 通信の種類・ポート番号: "TCP"・7860
- 操作: "接続を許可する"
- ルールの適用: プライベート
- 規則名: 任意の名前

を設定する
## 参考サイト
### Stable Diffusion (AUTOMATIC1111) をAPIで操作する ～WEB UI不要で任意のサービスと連携～
https://note.com/rcat999/n/n1beb8d75d334
### Stable Diffusion WebUI のWindows 10に同じローカルネットワーク上のPCやスマホからアクセスできるようにする
https://zenn.dev/garyuten/articles/229c126405de37
