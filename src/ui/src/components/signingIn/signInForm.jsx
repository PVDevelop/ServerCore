import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

import Panel from "react-bootstrap/lib/Panel";
import Form from "react-bootstrap/lib/Form";
import FormGroup from "react-bootstrap/lib/FormGroup";
import FormControl from "react-bootstrap/lib/FormControl";
import Col from "react-bootstrap/lib/Col";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";

import * as signingInActions from "../../actions/signingIn";

class SignInForm extends React.Component {
    render() {
        return (
            <Panel header="Вход в систему">
                <Form>
                    <FormGroup>
                        <Col sm={1} componentClass={ControlLabel}>Email</Col>
                        <Col sm={11}>
                            <FormControl
                                type="email"
                                placeholder="Введите почтовый адрес" 
                                value={this.props.email}
                                onChange={::this.onEmailChange}/>
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col sm={1} componentClass={ControlLabel}>Пароль</Col>
                        <Col sm={11}>
                            <FormControl
                                type="password"
                                placeholder="Введите пароль" 
                                value={this.props.password}
                                onChange={::this.onPasswordChange}/>
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col smOffset={1} sm={11}>
                            <Button type="submit">
                                Создать
                        </Button>
                        </Col>
                    </FormGroup>
                </Form>
            </Panel>
        );
    }

    onEmailChange(event) {
        this.props.signInActions.setEmail(event.target.value);
    }

    onPasswordChange(event) {
        this.props.signInActions.setPassword(event.target.value);
    }
}

function mapStateToProps(state) {
    return {
        email: state.signingIn.email,
        password: state.signingIn.password
    };
}

function mapDispatchToProps(dispatch) {
    return {
        signInActions: bindActionCreators(signingInActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SignInForm);