import React from "react";
import { connect } from "react-redux";
import { browserHistory } from "react-router";

import Header from "../components/header"

import * as validateTokenActions from "../actions/validateToken";
import * as signOutActions from "../actions/signOut";

class MainContainer extends React.Component {
    constructor(props) {
        super(props);

        this.onSignOutClicked = this.onSignOutClicked.bind(this);
        this.onSignInClicked = this.onSignInClicked.bind(this);
        this.onRegisterClicked = this.onRegisterClicked.bind(this);
    }
    render() {
        return (
            <div>
                <Header
                    isSignedIn={this.props.isSignedIn}
                    isSigningInFirstTime={this.props.isSigningInFirstTime}
                    onSignOutClicked={this.onSignOutClicked}
                    onSignInClicked={this.onSignInClicked}
                    onRegisterClicked={this.onRegisterClicked} />
                {this.props.children}
            </div>);
    }

    componentWillMount() {
        this.props.dispatch(validateTokenActions.validate());
    }

    onSignOutClicked() {
        this.props.dispatch(signOutActions.signOut());
    }

    onSignInClicked() {
        browserHistory.push("/signin");
    }

    onRegisterClicked() {
        browserHistory.push("/register");
    }
}

function mapStateToProps(state) {
    return {
        isSigningInFirstTime: state.user.everValidated === false,
        isSignedIn: state.user.currentUser != null
    };
}

export default connect(mapStateToProps)(MainContainer);