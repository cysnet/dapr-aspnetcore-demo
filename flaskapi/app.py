from flask import Flask
from dapr.clients import DaprClient

app = Flask(__name__)

@app.route("/")
def call_frontend():
    with DaprClient() as d:
        res = d.invoke_method('frontend','Dapr/ip',None)
        return res.text()


app.run(port=5003)