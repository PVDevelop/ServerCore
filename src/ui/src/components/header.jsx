import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router";
import Button from "react-bootstrap/lib/Button";

import * as signInActions from "../actions/signIn";

class Header extends React.Component {
    componentWillMount() {
        var options = {
            method: "put",
            credentials: "same-origin"
        };

        var url = "/api/tokens";

        console.log("Validating token at " + url);

        fetch(url, options)
            .then(response => {
                console.log(response);

                if (response.status == 200) {
                    console.log("Токен валиден, вы аутентифицированы.");
                    this.props.signInActions.setIsSignedIn(true);
                }
                else {
                    console.log("Токен невалиден, вы не аутентифицированы.");
                    this.props.signInActions.setIsSignedIn(false);
                }
            })
            .catch(err => {
                console.log(err);
                this.props.signInActions.setIsSignedIn(false);
            });
    }

    render() {
        const isSignedIn = this.props.isSignedIn;
        let content = null;

        if (isSignedIn) {
            content = (
                <span>
                    Вы вошли в систему
                    <Button type="submit" onClick={::this.onSignOutClicked}>
                        Выйти
                    </Button>
                </span >);
        }
        else {
            content = (
                <span>
                    Вы не вошли в систему
                    <Link to="signIn">Войти</Link>
                </span>);
        }

        return (
            <div>
                <div>
                    Привет!
                    {content}
                </div>
                <div>
                    {this.props.children}
                </div>
            </div>
        );
    }

    onSignOutClicked(e) {
        var options = {
            method: "put",
            credentials: "same-origin"
        };

        var url = "/api/users/sign_out";

        console.log("Signing out at " + url);

        fetch(url, options)
            .then(response => {
                console.log(response);

                if (response.status == 200) {
                    alert("Вы покинули систему.");
                    this.props.signInActions.setIsSignedIn(false);
                }
                else {
                    alert("Ошибки при выходе из системы.");
                }
            })
            .catch(err => {
                alert("Ошибки при выходе из системы.");
            });
    }
}

function mapStateToProps(state) {
    return {
        isSignedIn: state.signIn.isSignedIn
    };
}

function mapDispatchToProps(dispatch) {
    return {
        signInActions: bindActionCreators(signInActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Header);