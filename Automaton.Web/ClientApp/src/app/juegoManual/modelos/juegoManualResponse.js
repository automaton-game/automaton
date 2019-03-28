"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var juegoResponse_1 = require("../../juego/modelos/juegoResponse");
var JuegoManualResponse = /** @class */ (function (_super) {
    __extends(JuegoManualResponse, _super);
    function JuegoManualResponse() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return JuegoManualResponse;
}(juegoResponse_1.JuegoResponse));
exports.JuegoManualResponse = JuegoManualResponse;
//# sourceMappingURL=juegoManualResponse.js.map