import React from "react";
import { connect } from "react-redux";

import * as confirmUserActions from "../actions/confirmUser";

class ConfirmationContainer extends React.Component {
    render() {
        return (<div>Ожидание подтверждения пользователя...</div>);
    }

    componentDidMount() {
        this.props.dispatch(confirmUserActions.confirm(this.props.params.key));
    }
}

function mapStateToProps(state) {
    return {
    };
}

export default connect(mapStateToProps)(ConfirmationContainer);