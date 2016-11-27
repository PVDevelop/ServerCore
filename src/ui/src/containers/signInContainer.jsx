import React from "react";
import { connect } from "react-redux";

import SignIn from "../components/signIn";
import * as signInActions from "../actions/signIn";

class SignInContainer extends React.Component {
    render() {
        return (
            <SignIn
                email={this.props.email}
                password={this.props.password}
                isSigningIn={this.props.isSigningIn}
                onSignInClicked={::this.onSignInClicked }
                onEmailChanged={::this.onEmailChanged }
                onPasswordChanged={::this.onPasswordChanged } > 
            </SignIn>);
    }

    onSignInClicked() {
        this.props.dispatch(signInActions.signIn(this.props.email, this.props.password));
    }

    onEmailChanged(email) {
        this.props.dispatch(signInActions.setEmail(email));
    }

    onPasswordChanged(password) {
        this.props.dispatch(signInActions.setPassword(password));
    }
}

function mapStateToProps(state) {
    return {
        email: state.signIn.email,
        password: state.signIn.password,
        isSigningIn: state.signIn.signingIn
    };
}

export default connect(mapStateToProps)(SignInContainer);