import React from "react";
import { connect } from "react-redux";

import Header from "../components/header"

import * as validateTokenActions from "../actions/validateToken";
import * as signOutActions from "../actions/signOut";

class MainContainer extends React.Component {
    render() {
        return (
            <div>
                <Header isSignedIn={this.props.isSignedIn} onSignOutClicked={::this.onSignOutClicked}/>
                {this.props.children}
            </div>);
    }

    componentWillMount() {
        this.props.dispatch(validateTokenActions.validate());
    }

    onSignOutClicked() {
        this.props.dispatch(signOutActions.signOut());
    }
}

function mapStateToProps(state) {
    return {
        isSignedIn: state.user.currentUser != null
    };
}

export default connect(mapStateToProps)(MainContainer);