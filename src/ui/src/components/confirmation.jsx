import React from "react";
import { browserHistory } from "react-router";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

import * as signInActions from "../actions/signIn";
import * as confirmationActions from "../actions/confirmation";

class Confirmation extends React.Component {
    render() {
        return (<div>Ожидание подтверждения пользователя...</div>);
    }

    componentDidMount() {
        console.log("Confirmation did mount");

        var options = {
            method: "get",
            credentials: "same-origin",
            headers: {
                "Accept": "application/json"
            }
        };

        var url = "/api/confirmations/" + this.props.params.key;

        console.log("Sending confirmation at " + url);

        fetch(url, options)
            .then(response => {
                console.log(response);

                if (response.status == 200) {
                    alert("Пользователь подтвержден");
                    signInActions.setIsSignedIn(true);
                    browserHistory.push("/");
                }
                else {
                    alert("Ошибка подтверждения пользователя");
                }
                browserHistory.push("/");
            })
            .catch(err => {
                console.log(err);
                alert("Ошибка подтверждения пользователя");
                browserHistory.push("/");
            });
    }
}

function mapStateToProps(state) {
    return {
        isConfirming: state.confirmation.isConfirming
    };
}

function mapDispatchToProps(dispatch) {
    return {
        confirmationActions: bindActionCreators(confirmationActions, dispatch),
        signInActions: bindActionCreators(signInActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Confirmation);