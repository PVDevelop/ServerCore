import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router";
import Button from "react-bootstrap/lib/Button";

import * as validateTokenActions from "../actions/validateToken";
import * as signOutActions from "../actions/signOut";

class Header extends React.Component {
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
        this.props.dispatch(signOutActions.signOut());
    }

    componentWillMount() {
        this.props.dispatch(validateTokenActions.validate());
    }
}

function mapStateToProps(state) {
    return {
        isSignedIn: state.user.currentUser != null
    };
}

export default connect(mapStateToProps)(Header);