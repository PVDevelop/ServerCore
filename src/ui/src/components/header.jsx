import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router";

import * as signInActions from "../actions/signIn";

const NotSignedIn = () => {
    return (
        <span>
            Вы не вошли в систему
            <Link to="signIn">Войти</Link>
        </span>);
};

const SignedIn = () => {
    return (
        <span>
            Вы вошли в систему
            <Link to="signOut">Выйти</Link>
        </span>
    );
};

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
        return (
            <div>
                <div>
                    Привет!
                    {isSignedIn ? <SignedIn /> : <NotSignedIn />}
                </div>
                <div>
                    {this.props.children}
                </div>
            </div>
        );
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