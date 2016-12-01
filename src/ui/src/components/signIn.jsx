import React from "react";

import Panel from "react-bootstrap/lib/Panel";
import Form from "react-bootstrap/lib/Form";
import FormGroup from "react-bootstrap/lib/FormGroup";
import FormControl from "react-bootstrap/lib/FormControl";
import Col from "react-bootstrap/lib/Col";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";

import FormInput from "./formInput";
import FormSubmitButton from "./formSubmitButton";

export default class SignIn extends React.Component {
    constructor(props) {
        super(props);

        this.onSignInClicked = this.onSignInClicked.bind(this);
    }

    render() {
        var p = this.props;
        return (
            <form>
                <Col lg={4} lgOffset={4} md={6} mdOffset={3} sm={6} smOffset={3} xs={8} xsOffset={2}>
                    <Panel header="Вход в систему">
                    
                        <FormInput
                            controlId="inputEmail"
                            label="Почтовый адрес"
                            type="email"
                            placeholder="Введите почтовый адрес"
                            glyph="user"
                            value={this.props.email}
                            onChange={e => this.props.onEmailChanged(e.target.value)} />

                        <FormInput
                            controlId="inputPassword"
                            label="Пароль"
                            type="password"
                            placeholder="Введите пароль"
                            glyph="lock"
                            value={this.props.password}
                            onChange={e => this.props.onPasswordChanged(e.target.value)} />

                        <FormSubmitButton
                            text={"Войти"}
                            error={this.props.signInError}
                            disabled={this.props.isSigningIn === true}
                            onClicked={this.onSignInClicked}/>
                    </Panel >
                </Col>
            </form>
        );
    }

    onSignInClicked(e) {
        e.preventDefault();
        this.props.onSignInClicked();
    }
}