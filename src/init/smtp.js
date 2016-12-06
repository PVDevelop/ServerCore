const readline = require('readline');
const fs = require("fs");
const path = require("path");

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

const smtpPath = "../server/Microservices/Authentication/AuthenticationApp/smtp.json";

var smtp = {
    emailConfirmationProducer: {
        EnableSsl: "true",
        SenderAddress: "some@mail.com",
        SmtpHost: "smtp.some.com",
        SmtpPort: "25",
        Password: "password",
        UserName: "user"
    }
}

console.log("Setting up Smtp parameters (to allow sending user creation confirmations).");

readSmtp().
    catch(() => { }).
    then(() => readBoolParameter("Enable Ssl (true of false)", "EnableSsl")).
    then(() => readStringParameter("Sender address", "SenderAddress")).
    then(() => readStringParameter("Smtp host", "SmtpHost")).
    then(() => readIntParameter("Smtp port", "SmtpPort")).
    then(() => readStringParameter("Password", "Password")).
    then(() => readStringParameter("User name", "UserName")).
    then(() => writeResult()).
    catch(err => console.log(err)).
    then(() => rl.close());

function readSmtp() {
    return new Promise((resolve, reject) => {
        try {
            fs.readFile(smtpPath, "utf8", (err, data) => {
                if (err) reject(err);
                try {
                    let tempSmtp = JSON.parse(data);
                    smtp = Object.assign({}, smtp, tempSmtp);
                }
                catch (err) {
                    reject(err);
                }
                resolve();
            })
        } catch (err) {
            reject(err);
        }
    })
}

function readBoolParameter(displayName, paramName) {
    return readParameter(displayName, paramName, answer => {
        if (answer === "true") {
            smtp.emailConfirmationProducer[paramName] = "true";
        } else if (answer == "false") {
            smtp.emailConfirmationProducer[paramName] = "false";
        }
    });
}

function readIntParameter(displayName, paramName) {
    return readParameter(displayName, paramName, answer => {
        const intParseResult = parseInt(answer);
        if (intParseResult) {
            smtp.emailConfirmationProducer[paramName] = intParseResult.toString();
        }
    });
}

function readStringParameter(displayName, paramName) {
    return readParameter(displayName, paramName, answer => { smtp.emailConfirmationProducer[paramName] = answer });
}

function readParameter(displayName, paramName, processAnswer) {
    return new Promise((resolve, reject) => {
        let displayText = displayName + ":";
        rl.question(displayText, (answer) => {
            try {
                if (answer) {
                    processAnswer(answer);
                }
                resolve();
            }
            catch (err) {
                reject(err);
            }
        });
        const value = smtp.emailConfirmationProducer[paramName];
        rl.write(value.toString());
    })
}

function writeResult() {
    const json = JSON.stringify(smtp, null, 4);
    console.log(json);
    fs.writeFile(smtpPath, json, err => {
        if (err) {
            throw err;
        }
    });
}