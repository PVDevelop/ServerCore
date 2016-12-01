import React from "react";
import { connect } from "react-redux";

import SignIn from "../components/signIn";
import * as signInActions from "../actions/signIn";

class SignInContainer extends React.Component {
    constructor(props) {
        super(props);

        this.onSignInClicked = this.onSignInClicked.bind(this);
        this.onEmailChanged = this.onEmailChanged.bind(this);
        this.onPasswordChanged = this.onPasswordChanged.bind(this);
    }

    render() {
        return (
            <SignIn
                email={this.props.email}
                password={this.props.password}
                isSigningIn={this.props.isSigningIn}
                signInError={this.props.signInError}
                onSignInClicked={this.onSignInClicked}
                onEmailChanged={this.onEmailChanged}
                onPasswordChanged={this.onPasswordChanged} >
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
        isSigningIn: state.signIn.signingIn,
        signInError: state.signIn.error
    };
}

export default connect(mapStateToProps)(SignInContainer);